using System;
using System.Data;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace SqlWrapper {
    public class SELECT : SqlCommand, Expression{
        private bool distinct = false;
        private FROM from = null;
        private WHERE where = null;
        private LIMIT limit = null;
        private OrderBy orderBy = null;
        private GroupBy groupBy = null;

        private Collection<Expression> selectExpressions = new Collection<Expression>();



        public SELECT(){
            
        }

        //-------------Adding expressions ----------------

        public SELECT DISTINCT() {

            this.distinct = true;

            return this;
        }

        public SELECT col(Expression expression){
            

            this.selectExpressions.Add(expression);
            return this;
        }

        //public SELECT  col(string stringLiteral){
            

        //    this.selectExpressions.Add(new StringLiteral(stringLiteral));

        //    return this;
        //}

        public SELECT star(){

            this.selectExpressions.Add(new Star());

            return this;
        }

        public SELECT FROM(Expression expression){
            this.from = new FROM(expression);

            return this;
        }


        public SELECT FROM(params JOIN[] joins){

            Collection<JOIN> joinCollection = new Collection<JOIN>();

            foreach(JOIN join_ in joins){

                joinCollection.Add(join_);
            }

            FROM newFrom = new FROM();

            newFrom.addJoins(joinCollection);

            this.from = newFrom;
           
            return this;
        }



        public SELECT FROMJOIN(Expression table1, Expression table2, Expression joinExpression){

            this.FROM(
                new JOIN(EJoinType.Join, table1, table2, joinExpression)
                     );

            return this;
        }

        public SELECT FROMJOINLeft(Expression table1, Expression table2, Expression joinExpression) {

            this.FROM(
                      new JOIN(EJoinType.Left, table1, table2, joinExpression)
                     );

            return this;
        }

        public SELECT FROMJOINRight(Expression table1, Expression table2, Expression joinExpression) {

            this.FROM(
                      new JOIN(EJoinType.Right, table1, table2, joinExpression)
                     );

            return this;
        }

        public SELECT FROMJOINFull(Expression table1, Expression table2, Expression joinExpression) {

            this.FROM(
                      new JOIN(EJoinType.Full, table1, table2, joinExpression)
                     );

            return this;
        }



        public SELECT WHERE(Expression expression){
            this.where = new WHERE(expression);

            return this;
        }

        public SELECT WHEREEquals(COLUMN column, Expression expression){

            this.where = new SqlWrapper.WHERE(
                new OperatorExpression(
                    column,
                    new Operator(EOperatorType.Equals),
                    expression
                )
            );

            return this;
        }


        public SELECT ORDERBY(Expression expression, EOrderType orderType){

            if(this.orderBy == null){
                this.orderBy = new OrderBy();
            }

            this.orderBy.addOrderby(expression, orderType);
            return this;
        }

        public SELECT ORDERBY(Expression expression){
            
            if (this.orderBy == null) {
                this.orderBy = new OrderBy();
            }

            this.orderBy.addOrderby(expression, EOrderType.none);

            return this;
        }

        public SELECT GROUPBY(Expression expression){
            if(this.groupBy == null){

                this.groupBy = new GroupBy(expression);
            }else{

                this.groupBy.addColumn(expression);
            }

            return this;
        }


        //--------------RENDER------------------------
        public string render(ERenderType renderType){
            RenderContext renderContext = new RenderContext(renderType);

            return this.render(renderContext);
        }



        public override string render(RenderContext renderContext){
            
            string queryString = "SELECT";

            if (this.distinct) {

                queryString += " DISTINCT";
            }


            if (this.selectExpressions.Count != 0) {

                queryString += " ";

                bool isFirst = true;

                foreach (Expression expression in this.selectExpressions) {

                    if (isFirst) {

                        queryString += expression.render(renderContext);

                        isFirst = false;
                    } else {

                        queryString += ", " + expression.render(renderContext);

                    }


                }

            }

            if (this.from != null) {

                queryString += " " + this.from.render(renderContext);

            }

            if (this.where != null){

                queryString +=  " "  + this.where.render(renderContext);
            }

            if (this.limit != null){

                queryString += " " + this.limit.render(renderContext);
            }

            if (this.groupBy != null){

                queryString += " " + this.groupBy.render(renderContext);
            }

            if (this.orderBy != null){

                queryString += " " + this.orderBy.render(renderContext);
            }

            return queryString += " ;";
        }




    }
}
