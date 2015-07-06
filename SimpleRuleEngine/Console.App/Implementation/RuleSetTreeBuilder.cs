using Console.App.Abstracts;
using Console.App.Abstracts.Specification;
using Console.App.Grammar;
using Console.App.Implementation.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Console.App.Implementation
{
    public class RuleSetTreeBuilder : RuleSetBaseListener
    {
        protected IJsonParser jsonPathParser;

        public RuleSet ruleSet { get; private set; }

        private Stack<CompositeSpecification> specifications = new Stack<CompositeSpecification>();

        private Stack<ArithmeticExpression> leftArithmeticExpressions = new Stack<ArithmeticExpression>();
        private Stack<ArithmeticExpression> rightArithmeticExpressions = new Stack<ArithmeticExpression>();
        private Stack<ArithmeticExpression> looseArithmeticExpressions = new Stack<ArithmeticExpression>();

        private Stack<ArithmeticExpression> arithmeticExpressions;

        public RuleSetTreeBuilder(IJsonParser jsonPathParser)
        {
            this.jsonPathParser = jsonPathParser;
            this.arithmeticExpressions = looseArithmeticExpressions;
        }

        protected List<String> getArray(RuleSetParser.String_arrayContext string_array)
        {
            String arrayString = string_array.GetText()
                .Replace("(", "")
                .Replace(")", "");

            return Regex.Split(arrayString, "\\s*,\\s*").ToList();
        }

        protected ArithmeticExpression getArithmeticExpression(RuleSetParser.Left_arithmetic_exprContext left_arithmetic_exprContext)
        {
            if (leftArithmeticExpressions.Count == 1)
            {
                return leftArithmeticExpressions.Pop();
            }
            throw new ArgumentException("Could not determine ArithmeticExpression from arithmetic_exprContext: " + left_arithmetic_exprContext.GetText());
        }

        protected ArithmeticExpression getArithmeticExpression(RuleSetParser.Right_arithmetic_exprContext right_arithmetic_exprContext)
        {
            if (rightArithmeticExpressions.Count == 1)
            {
                return rightArithmeticExpressions.Pop();
            }
            throw new ArgumentException("Could not determine ArithmeticExpression from arithmetic_exprContext: " + right_arithmetic_exprContext.GetText());
        }

        protected ArithmeticExpression getArithmeticExpression(RuleSetParser.Numeric_exprContext numeric_exprContext)
        {
            if (looseArithmeticExpressions.Count > 0)
            {
                return looseArithmeticExpressions.Pop();
            }
            throw new ArgumentException("Could not determine ArithmeticExpression from numeric_exprContext: " + numeric_exprContext.GetText());
        }

        protected String getValueExpression(RuleSetParser.Value_exprContext value_expr)
        {
            if (value_expr.jsonpath_expr() != null)
            {
                return getJsonPathExpression(value_expr.jsonpath_expr());
            }

            return value_expr.GetText();
        }

        protected String getJsonPathExpression(RuleSetParser.Jsonpath_exprContext jsonpath_expr)
        {
            return jsonpath_expr.GetText();
        }

        protected void exitRealArithmeticExpression(ArithmeticOperatorType Operator)
        {
            ArithmeticExpression right = this.arithmeticExpressions.Pop();
            ArithmeticExpression left = this.arithmeticExpressions.Pop();
            RealArithmeticExpression expr = new RealArithmeticExpression(Operator, left, right);
            this.arithmeticExpressions.Push(expr);
        }
        public override void EnterRule_set(RuleSetParser.Rule_setContext context)
        {
            this.ruleSet = new RuleSet();
        }
        public override void ExitRule_set(RuleSetParser.Rule_setContext context)
        {
            while (specifications.Count > 0)
            {
                ruleSet.AddRule(specifications.Pop());
            }
        }
        public override void ExitLogicalExpressionAnd(RuleSetParser.LogicalExpressionAndContext context)
        {
            var right = specifications.Pop();
            var left = specifications.Pop();
            this.specifications.Push(new AndSpecification(jsonPathParser, left, right));
        }
        public override void ExitLogicalExpressionOr(RuleSetParser.LogicalExpressionOrContext context)
        {
            var right = specifications.Pop();
            var left = specifications.Pop();
            this.specifications.Push(new OrSpecification(jsonPathParser, left, right));
        }
        public override void ExitLogicalExpressionNot(RuleSetParser.LogicalExpressionNotContext context)
        {
            var notspecification = specifications.Pop();
            this.specifications.Push(new NotSpecification(jsonPathParser, notspecification));
        }
        public override void ExitTotalledNumericGreaterThanComparisonSpecificationExpression(RuleSetParser.TotalledNumericGreaterThanComparisonSpecificationExpressionContext context)
        {
            var left = new TotalFromJsonPathExpression(jsonPathParser, context.jsonpath_expr().GetText());
            ArithmeticExpression right = (context.right_arithmetic_expr() != null) ? getArithmeticExpression(context.right_arithmetic_expr())
                : getArithmeticExpression(context.numeric_expr());

        }

        public override void ExitTotalledNumericLessThanComparisonSpecificationExpression(RuleSetParser.TotalledNumericLessThanComparisonSpecificationExpressionContext ctx)
        {
            ArithmeticExpression left = new TotalFromJsonPathExpression(jsonPathParser, ctx.jsonpath_expr().GetText());
            ArithmeticExpression right = (ctx.right_arithmetic_expr() != null) ? getArithmeticExpression(ctx.right_arithmetic_expr()) 
                : getArithmeticExpression(ctx.numeric_expr());

            specifications.Push(new LessThan(jsonPathParser, left, right));
        }


        public override void ExitNumericGreaterThanComparisonSpecificationExpression(RuleSetParser.NumericGreaterThanComparisonSpecificationExpressionContext ctx)
        {
            ArithmeticExpression right = (ctx.left_arithmetic_expr() != null) ? getArithmeticExpression(ctx.left_arithmetic_expr()) 
                : getArithmeticExpression(ctx.numeric_expr(0));
            ArithmeticExpression left = (ctx.right_arithmetic_expr() != null) ? getArithmeticExpression(ctx.right_arithmetic_expr()) 
                : getArithmeticExpression(ctx.numeric_expr()[(ctx.numeric_expr().Count - 1)]);

            specifications.Push(new GreaterThan(jsonPathParser, left, right));
        }


        public override void ExitNumericLessThanComparisonSpecificationExpression(RuleSetParser.NumericLessThanComparisonSpecificationExpressionContext ctx)
        {
            ArithmeticExpression right = (ctx.left_arithmetic_expr() != null) ? getArithmeticExpression(ctx.left_arithmetic_expr()) 
                : getArithmeticExpression(ctx.numeric_expr(0));
            ArithmeticExpression left = (ctx.right_arithmetic_expr() != null) ? getArithmeticExpression(ctx.right_arithmetic_expr()) : 
                getArithmeticExpression(ctx.numeric_expr()[(ctx.numeric_expr().Count - 1)]);

            specifications.Push(new LessThan(jsonPathParser, left, right));
        }

        public override void ExitNumericEqualComparisonSpecificationExpression(RuleSetParser.NumericEqualComparisonSpecificationExpressionContext ctx)
        {
            ArithmeticExpression right =  getArithmeticExpression(ctx.numeric_expr(0));
            ArithmeticExpression left =   getArithmeticExpression(ctx.numeric_expr()[(ctx.numeric_expr().Count - 1)]);

            specifications.Push(new Equals(jsonPathParser, left, right));
        }

        public override void ExitStringEqualsComparisonSpecificationExpression(RuleSetParser.StringEqualsComparisonSpecificationExpressionContext ctx)
        {
            specifications.Push(new StringEquals(jsonPathParser, getValueExpression(ctx.value_expr()), ctx.string_comparison_value().GetText()));
        }


        public override void ExitStringContainsComparisonSpecificationExpression(RuleSetParser.StringContainsComparisonSpecificationExpressionContext ctx)
        {
            specifications.Push(new StringContains(jsonPathParser, getValueExpression(ctx.value_expr()), ctx.string_comparison_value().GetText()));
        }


        public override void ExitBooleanIsTrueComparisonSpecificationExpression(RuleSetParser.BooleanIsTrueComparisonSpecificationExpressionContext ctx)
        {
            specifications.Push(new IsTrue(jsonPathParser, getValueExpression(ctx.value_expr())));
        }


        public override void ExitBooleanIsFalseComparisonSpecificationExpression(RuleSetParser.BooleanIsFalseComparisonSpecificationExpressionContext ctx)
        {
            specifications.Push(new NotSpecification(jsonPathParser, new IsTrue(jsonPathParser, getValueExpression(ctx.value_expr()))));
        }


        public override void ExitArrayIncludesOneComparisonSpecificationExpression(RuleSetParser.ArrayIncludesOneComparisonSpecificationExpressionContext ctx)
        {
            specifications.Push(new ArrayIncludeOne(jsonPathParser, getValueExpression(ctx.value_expr()), getArray(ctx.string_array())));
        }


        public override void EnterLeft_arithmetic_expr(RuleSetParser.Left_arithmetic_exprContext ctx)
        {
            leftArithmeticExpressions.Clear();
            arithmeticExpressions = leftArithmeticExpressions;
        }


        public override void ExitLeft_arithmetic_expr(RuleSetParser.Left_arithmetic_exprContext ctx)
        {
            arithmeticExpressions = looseArithmeticExpressions;
        }


        public override void EnterRight_arithmetic_expr(RuleSetParser.Right_arithmetic_exprContext ctx)
        {
            rightArithmeticExpressions.Clear();
            arithmeticExpressions = rightArithmeticExpressions;
        }


        public override void ExitRight_arithmetic_expr(RuleSetParser.Right_arithmetic_exprContext ctx)
        {
            arithmeticExpressions = looseArithmeticExpressions;
        }
        

        public override void ExitTotalledJsonPathExpression(RuleSetParser.TotalledJsonPathExpressionContext ctx)
        {
            this.arithmeticExpressions.Push(new TotalFromJsonPathExpression(jsonPathParser, ctx.total_expr().jsonpath_expr().GetText()));
        }


        public override void ExitJsonPathExpression(RuleSetParser.JsonPathExpressionContext ctx)
        {
            this.arithmeticExpressions.Push(new NumericField(jsonPathParser, ctx.GetText()));
        }


        public override void ExitNumericConstant(RuleSetParser.NumericConstantContext ctx)
        {
            this.arithmeticExpressions.Push(new NumericConstant(float.Parse(ctx.GetText())));
        }


        public override void ExitArithmeticExpressionMult(RuleSetParser.ArithmeticExpressionMultContext ctx)
        {
            exitRealArithmeticExpression(ArithmeticOperatorType.multiply);
        }


        public override void ExitArithmeticExpressionDiv(RuleSetParser.ArithmeticExpressionDivContext ctx)
        {
            exitRealArithmeticExpression(ArithmeticOperatorType.divide);
        }


        public override void ExitArithmeticExpressionPlus(RuleSetParser.ArithmeticExpressionPlusContext ctx)
        {
            exitRealArithmeticExpression(ArithmeticOperatorType.add);
        }


        public override void ExitArithmeticExpressionMinus(RuleSetParser.ArithmeticExpressionMinusContext ctx)
        {
            exitRealArithmeticExpression(ArithmeticOperatorType.subtract);
        }
        
    }
}
