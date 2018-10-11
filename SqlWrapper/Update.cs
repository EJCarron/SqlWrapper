using System;
using System.Data;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SqlWrapper {
    public class UPDATE : SqlCommand, IMySqlRenderable {
        private DataTable table;
        private Collection<UpdatePair> pairs = new Collection<UpdatePair>();
        private WHERE where;
        private JOIN join = null;

        public UPDATE(DataTable table) {
            this.table = table;
        }

        public UPDATE addValuePair (COLUMN column, Expression value){

            this.pairs.Add(new UpdatePair(column,value));

            return this;
        }

        public UPDATE WHERE (Expression expression){

            this.where = new SqlWrapper.WHERE(expression);

            return this;
        }


        public UPDATE WHEREEQUALS(COLUMN col, Expression expression){

            this.where = new SqlWrapper.WHERE(new OperatorExpression()
                                              .addExpression(col)
                                              .Equals()
                                              .addExpression(expression)
                                             );

            return this;
        }


        public UPDATE JOIN(DataTable table, Expression expression){

            this.join = new SqlWrapper.JOIN(EJoinType.Full, null, table, expression);

            return this;

        }

        public string render(ERenderType renderType){

            return this.render(new RenderContext(renderType));
        }


        public override string render(RenderContext renderContext){
            string renderString = "UPDATE " + this.table.render(renderContext);

            if(this.join != null){


                renderString += this.join.render(renderContext) + " ";
            }


            string setString = "SET ";

            string pairsString = "";

            foreach (UpdatePair pair in this.pairs){

                pairsString += (pairsString.Length == 0 ? "" : " , ") + pair.render(renderContext);

            }

            setString += pairsString;

            renderString += " " + setString;

            string whereString = this.where.render(renderContext);

            return renderString += " " + whereString + ";";
        }


    }


    public class UpdatePair :IMySqlRenderable{
        COLUMN column;
        Expression value;

        public UpdatePair(COLUMN column, Expression value){

            this.column = column;
            this.value = value;
        }

        public string render(RenderContext renderContext){
            return this.column.render(renderContext) + " = " + this.value.render(renderContext);
        }
    }
}
