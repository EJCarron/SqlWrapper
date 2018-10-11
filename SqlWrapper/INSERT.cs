using System;
using System.Data;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace SqlWrapper {
    public class INSERTINTO : SqlCommand ,IMySqlRenderable{
        private DataTable table;
        private Collection<COLUMN> columns = new Collection<COLUMN>();
        private Collection<Expression> values = new Collection<Expression>();
        private Expression whereNotExists = null;



        public INSERTINTO(DataTable table) {
            
            this.table = table;
        }


        public INSERTINTO ValuePair(COLUMN column, Expression value){
            
            this.columns.Add(column);
            this.values.Add(value);

            return this;
        }

        public INSERTINTO Value(Expression value){
            this.values.Add(value);
            return this;
        }

        public INSERTINTO WHERENOTEXISTS(Expression expression){

            this.whereNotExists = expression;

            return this;
        }

        public string render(ERenderType renderType){

            RenderContext renderContext = new RenderContext(renderType);

            return this.render(renderContext);
        }

        public override string render(RenderContext renderContext){

            string renderString = "INSERT INTO " + this.table.render(renderContext);

            if (this.columns.Count != 0) {

                string columnNames = "";

                foreach (COLUMN column in this.columns) {

                    columnNames += ( columnNames.Length == 0?"":" , ") + column.render(renderContext);
                
                }

                renderString += " ( " + columnNames + " )";
            }

            string MainValueString = " VALUES ";

            string valuesString = "(";

            bool isFirstCol = true;

            foreach (Expression value_ in this.values) {

                if (isFirstCol) {
                    valuesString += value_.render(renderContext);
                    isFirstCol = false;
                } else {

                    valuesString += ", " + value_.render(renderContext);
                }
            }

            valuesString += ")";

            MainValueString +=  " " + valuesString;

            renderString += " " + MainValueString;

            if(this.whereNotExists == null){
                renderString += " ;";
            }else{

                renderString += " WHERE NOT EXISTS (" + this.whereNotExists.render(renderContext);

                renderString = renderString.Remove(renderString.Length - 1);

                renderString += ");";

            }

            return renderString;
        }


    }
}
