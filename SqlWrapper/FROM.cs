using System;
using System.Collections.ObjectModel;

namespace SqlWrapper {
    public class FROM : IMySqlRenderable{
        private Expression expression;
        private Collection<JOIN> joins = null;

        public FROM (){
            
        }

        public FROM(Expression expression) {

            this.expression = expression;
        }

        public void addJoins(Collection<JOIN> joins){
            
            this.joins = joins;
        }

        public string render(RenderContext renderContext) {

            if (this.joins == null) {

                return "FROM " + this.expression.render(renderContext);
            }else{

                string fromString = "FROM ";

                foreach (JOIN join_ in this.joins){
                    fromString += join_.render(renderContext) + " ";
                }

                return fromString;
            }




        }

    }
}
