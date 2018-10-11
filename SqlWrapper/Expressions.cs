using System;
using System.Collections.ObjectModel;
namespace SqlWrapper {


    //+++++++++++++++++++Expression++++++++++++++++++++++

    public interface Expression : IMySqlRenderable {



    }






    //+++++++++++++++++++Column+++++++++++++++++++

    public class COLUMN : Expression {
        public string columnName;
        private string tableName;


        //-------------Constructor-------------

        public COLUMN(string tableName, string columnName) {

            this.columnName = columnName;
            this.tableName = tableName;
        }
        //-------------Render----------------------

        public string render(RenderContext renderContext) {
            return " " + this.tableName + "." + this.columnName;
        }

        public static implicit operator string(COLUMN COL){
            return COL.columnName;
        }
    }


    //++++++++++++++++++++DataTable++++++++++++++++++++


    public class DataTable : Expression{
        private string tableName;

        public DataTable(string tableName) {
            this.tableName = tableName;
        }

        public string render(RenderContext renderContext){
            
            return this.tableName;
        }
    }


    //+++++++++++++++++++Literals+++++++++++++++++++


    public class Star : Expression{

        public string render(RenderContext renderContext){

            return "*";
        }
    }


    public class BoolLiteral : Expression {
        private bool BOOL;

        public BoolLiteral(bool BOOL) {

            this.BOOL = BOOL;
        }

        public string render(RenderContext renderContext) {
            if (this.BOOL) {

                return "TRUE";
            }

            return "FALSE";

        }
    }



    public class IntLiteral :Expression {
        private int literal;


        public IntLiteral(int literal){

            this.literal = literal;
        }

        public string render(RenderContext renderContext){
            
            return this.literal.ToString();
        }
    }

    public class DateLiteral : Expression{
        private DateTime literal;


        public DateLiteral(DateTime literal) {

            this.literal = literal;
        }

        public string render(RenderContext renderContext) {

            return "'" +this.literal.ToString("yyyy-MM-dd") + "'";
        }

    }

    public class StringLiteral : Expression {
        private string literal;
        private int paramNum;


        //---------------Constructors-----------

        public StringLiteral(string literal) {
            this.literal = literal;
        }






        //--------------Render--------------

        public string render(RenderContext renderContext) {
            renderContext.paramCounter++;
            this.paramNum = renderContext.paramCounter;

            renderContext.addParamPair(this.paramNum, this.literal);

            switch (renderContext.renderType) {

                case ERenderType.NonParamed:
                    return " \'" + this.literal + "\'";

                case ERenderType.Paramed:
                    return " @" + this.paramNum;

            }


            return null;
        }



    }


    //+++++++++++++++++++Operator Expression +++++++++++++++++++++

    public class OperatorExpression : Expression{
        private Collection<Expression> expressions = new Collection<Expression>();


        public OperatorExpression(){
            
        }

        public OperatorExpression(Expression expression1, Operator operator_, Expression expression2){
            this.expressions.Add(expression1);
            this.expressions.Add(operator_);
            this.expressions.Add(expression2);
        }

        public OperatorExpression addExpression(Expression expression){
            this.expressions.Add(expression);
            return this;
        }

        public OperatorExpression addOperator(Operator operator_){
            this.expressions.Add(operator_);
            return this;
        }

        public OperatorExpression AND(){
            this.expressions.Add(new Operator(EOperatorType.And));
            return this;
        }

        public OperatorExpression BETWEEN(){
            this.expressions.Add(new Operator(EOperatorType.Between));
            return this;
        }

        public OperatorExpression DoesNotEqual() {

            this.expressions.Add(new Operator(EOperatorType.DoesNotEqual));

            return this;
        }




        public OperatorExpression Equals(){

            this.expressions.Add(new Operator(EOperatorType.Equals));

            return this;
        }

        public OperatorExpression GreaterThan(){

            this.expressions.Add(new Operator(EOperatorType.GreaterThan));
            return this;
        }

        public OperatorExpression GreaterThanOrEqual(){
            this.expressions.Add(new Operator(EOperatorType.GreaterThanOrEqual));
            return this;
        }

        public OperatorExpression Like(){
            this.expressions.Add(new Operator(EOperatorType.Like));
            return this;
        }

        public OperatorExpression OR(){
            this.expressions.Add(new Operator(EOperatorType.Or));
            return this;
        }

        public OperatorExpression NOT(){
            this.expressions.Add(new Operator(EOperatorType.Not));
            return this;
        }

        //-------------------Render------------------------

        public string render(RenderContext renderContext) {

            string renderString = "";

            int counter = 0;

            foreach(Expression expression in this.expressions){
                if (counter == this.expressions.Count){

                    renderString += expression.render(renderContext);
                }else{

                    renderString += expression.render(renderContext) + " ";

                    counter++;
                }

            }

            return renderString;
        }
    }









    //+++++++++++++++++++++++Operator++++++++++++++++++++++++

    public class Operator : Expression{
        private string OperatorString;

        public Operator(EOperatorType operatorType){

            switch(operatorType){

                case EOperatorType.And:
                    this.OperatorString = OperatorKeys.AND;
                    break;

                case EOperatorType.Between: 
                    this.OperatorString = OperatorKeys.between;
                    break;

                case EOperatorType.DoesNotEqual:
                    this.OperatorString = OperatorKeys.doesNotEqual;
                    break;

                case EOperatorType.Equals:
                    this.OperatorString = OperatorKeys.equals;
                    break;
                
                case EOperatorType.GreaterThan:
                    this.OperatorString = OperatorKeys.greaterThan;
                    break;

                case EOperatorType.GreaterThanOrEqual:
                    this.OperatorString = OperatorKeys.greaterThanOrEqual;
                    break;

                case EOperatorType.In:
                    this.OperatorString = OperatorKeys.In;
                    break;

                case EOperatorType.LessThan:
                    this.OperatorString = OperatorKeys.lessThan;
                    break;

                case EOperatorType.LessThanOrEqual:
                    this.OperatorString = OperatorKeys.lessThanOrEqual;
                    break;

                case EOperatorType.Like:
                    this.OperatorString = OperatorKeys.Like;
                    break;

                case EOperatorType.Or:
                    this.OperatorString = OperatorKeys.OR;
                    break;

                case EOperatorType.Not:
                    this.OperatorString = OperatorKeys.NOT;
                    break;
            }

        }

        public string render(RenderContext renderContext) {

            return this.OperatorString;
        }
    }


    //++++++++++++++++IS NULL+++++++++++++++++++

    public class IsNUll : Expression{
        private Expression expression;


        public IsNUll(Expression expression){

            this.expression = expression;

        }

        public string render(RenderContext renderContext) {

                    
                    return this.expression.render(renderContext) + " IS NULL";

        }
    }

    public class IsNotNUll : Expression {
        private Expression expression;


        public IsNotNUll(Expression expression) {

            this.expression = expression;

        }

        public string render(RenderContext renderContext) {


            return this.expression.render(renderContext) + " IS NOT NULL";

        }
    }


    //+++++++++++++++++++++++++ AS +++++++++++++++++++++++

    public class AS : Expression{
        private Expression expression;
        private Expression newColumn;

        public AS (Expression expression, COLUMN newColumn){

            this.expression = expression;
            this.newColumn = newColumn;
        }

        public AS(Expression expression, DataTable newTable) {

            this.expression = expression;
            this.newColumn = newTable;
        }


        public AS(Expression expression, string newColumn){
            this.expression = expression;
            this.newColumn = new COLUMN(null, newColumn);
        }

        public string render(RenderContext renderContext) {

            return this.expression.render(renderContext) + " AS " + this.newColumn.render(renderContext);
            
        }


    }


    //+++++++++++++++++++++++++ MATHS EXPRESSION +++++++++++++++++++++++

    public class MATHS : Expression{
        private Expression expression;
        private EMathsType mathsType;

        public MATHS(EMathsType mathsType, Expression expression){
            this.expression = expression;
            this.mathsType = mathsType;
        }

        public string render(RenderContext renderContext){

            string functionName = "";

            string functionBody = "(" + this.expression.render(renderContext) + ")";

            switch(this.mathsType){

                case EMathsType.AVG:
                    functionName = "AVG";
                    break;

                case EMathsType.COUNT:
                    functionName = "COUNT";
                    break;

                case EMathsType.MAX:
                    functionName = "MAX";
                    break;

                case EMathsType.MIN:
                    functionName = "MIN";
                    break;

                case EMathsType.SUM:
                    functionName = "SUM";
                    break;

            }

            return functionName + functionBody;

        }
    }

    public class NOW : Expression {
        public string render(RenderContext renderContext) {
            return "NOW()";
        }
    }

    //++++++++++++++++++++++++++ IN Expression +++++++++++++++++++++=

    public class IN : Expression{
        private Expression operativeExpression;
        private Collection<Expression> inExpressions = new Collection<Expression>();
        private bool notValue;

        public IN (Expression operativeExpression, params Expression[] inExpressions){

            this.notValue = false;

            this.operativeExpression = operativeExpression;

            foreach (Expression expression in inExpressions) {
                this.inExpressions.Add(expression);
            }

        }

        public IN(bool notValue, Expression operativeExpression, params Expression[] inExpressions){

            this.notValue = notValue;

            this.operativeExpression = operativeExpression;

            foreach (Expression expression in inExpressions) {
                this.inExpressions.Add(expression);
            }

        }

        public string render(RenderContext renderContext) {

            string functionName = this.operativeExpression.render(renderContext);

            if(this.notValue){

                functionName += " IN ";
            }else{

                functionName += " NOT IN ";
            }

            string functionBody = "(";

            bool isFirst = true;

            foreach(Expression expression in this.inExpressions){

                if(isFirst){

                    functionBody += expression.render(renderContext);
                    isFirst = false;
                }else{

                    functionBody += ", " + expression.render(renderContext);
                }
            }

            functionBody += ")";

            return functionName + functionBody;

        }

    }


    //++++++++++++++++++++++++++++++JOIN++++++++++++++++++++++++++

    public class JOIN : Expression{
        private EJoinType joinType;
        private Expression table1;
        private Expression table2;
        private Expression joinExpression;

        public JOIN (EJoinType joinType, Expression table1, Expression table2, Expression joinExpression){

            this.joinType = joinType;
            this.table1 = table1;
            this.table2 = table2;
            this.joinExpression = joinExpression;
            this.joinType = joinType;
        }

        public string render(RenderContext renderContext) {

            string renderString = "";

            if (this.table1 != null){
                renderString += this.table1.render(renderContext);
            }

            switch(this.joinType){

                case EJoinType.Full:
                    renderString += " ";
                    break;


                case EJoinType.Inner:
                    renderString += " INNER";
                    break;


                case EJoinType.Left:
                    renderString += " LEFT";
                    break;


                case EJoinType.Right:
                    renderString += " RIGHT";
                    break;

                case EJoinType.Join:
                    break;
            }

            renderString += " JOIN "; 
            renderString += this.table2.render(renderContext);
            renderString += " ON ";
            renderString += this.joinExpression.render(renderContext);


            return renderString;

        }
    }

   


}
