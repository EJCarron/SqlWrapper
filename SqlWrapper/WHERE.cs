using System;
using System.Collections.ObjectModel;
namespace SqlWrapper {
    public class WHERE : IMySqlRenderable{
        private Expression expression;


        public WHERE(Expression expression) {

            this.expression = expression;
        }

        public string render(RenderContext renderContext){

            return "WHERE " + this.expression.render(renderContext);

        }

    }

}
