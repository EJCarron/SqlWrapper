using System;
using System.Data;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SqlWrapper {
    public class DELETE : SqlCommand, IMySqlRenderable{
        private FROM from;
        private WHERE where;

        public DELETE() {
        }

        public DELETE FROM(DataTable table){

            this.from = new FROM(table);
            return this;
        }

        public DELETE WHERE(Expression expression){

            this.where = new WHERE(expression);
            return this;
        }

        public string render(ERenderType renderType){
            return this.render(new RenderContext(renderType));
        }

        public override string render(RenderContext renderContext){

            string renderString = "DELETE ";

            renderString += " " + this.from.render(renderContext);

            renderString += " " + this.where.render(renderContext);

            renderString += ";";

            return renderString;
        }



    }
}
