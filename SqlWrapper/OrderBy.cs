using System;
using System.Collections.ObjectModel;

namespace SqlWrapper {
    public class OrderBy : IMySqlRenderable {
        private Collection<OrderByObj> orderObjs = new Collection<OrderByObj>();

        public OrderBy() {
            
        }

        public void addOrderby(Expression expression, EOrderType orderType){

            this.orderObjs.Add(new OrderByObj(expression,orderType));

        }

        public void addOrderby(OrderByObj orderObj){
            this.orderObjs.Add(orderObj);
        }

        public string render(RenderContext renderContext) {
            string renderString = "ORDER BY ";

            bool isFirst = true;

            foreach (OrderByObj orderObj in orderObjs) {
                if (isFirst) {

                    renderString += orderObj.render(renderContext);

                    isFirst = false;
                } else {

                    renderString += ", " + orderObj.render(renderContext);
                }
            }

            return renderString;

        }
           
    }


    public class OrderByObj : IMySqlRenderable{
        private Expression expression;
        private EOrderType orderType;

        public OrderByObj(Expression expression, EOrderType orderType) {
            this.expression = expression;
            this.orderType = orderType;
        }

        public string render(RenderContext renderContext) {

            string renderString = this.expression.render(renderContext) + " ";

            switch (this.orderType) {

                case EOrderType.Asc:
                    renderString += "ASC";
                    break;

                case EOrderType.Desc:
                    renderString += "DESC";
                    break;

                case EOrderType.none:
                    break;
            }

            return renderString;
        }


    }
}
