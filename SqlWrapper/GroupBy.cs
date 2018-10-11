using System;
using System.Collections.ObjectModel;

namespace SqlWrapper {
    public class GroupBy : IMySqlRenderable {
        private Collection<Expression> columns = new Collection<Expression>();

        public GroupBy(Expression expression) {
            this.columns.Add(expression);
        }

        public void addColumn(Expression expression){

            this.columns.Add(expression);
        }

        public string render(RenderContext renderContext){

            string renderString = "GROUP BY ";

            bool isFirst = true;

            foreach(Expression expression in this.columns){

                if(isFirst){
                    renderString += expression.render(renderContext);
                }else{

                    renderString += ", " + expression.render(renderContext);
                }
            }

            return renderString;
        }
    }

}
