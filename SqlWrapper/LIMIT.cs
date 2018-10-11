using System;
namespace SqlWrapper {
    public class LIMIT : IMySqlRenderable{
        private Expression expression;

        public LIMIT (Expression expression){
            this.expression = expression;
        } 

        public string render(RenderContext renderContext){

            string renderString = "LIMIT ";

            renderString += this.expression.render(renderContext);

            return renderString;
        }



    }
}
