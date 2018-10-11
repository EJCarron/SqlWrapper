using System;
using System.Collections.ObjectModel;

namespace SqlWrapper {
    public class RenderContext {
        public int paramCounter;
        public ERenderType renderType;
        public Collection<ParamPair> paramPairs = new Collection<ParamPair>();


        public RenderContext (ERenderType renderType){
            this.paramCounter = 0;
            this.renderType = renderType;
        }

        public void addParamPair(int paramNum, string userInput){
            this.paramPairs.Add(new ParamPair(paramNum,userInput));
        }

    }

    public class ParamPair{
        public string parameter;
        public string userInput;

        public ParamPair(int paramNum, string userInput){
            
            this.userInput = userInput;

            this.parameter = "@" + paramNum.ToString();
        }
    }

}
