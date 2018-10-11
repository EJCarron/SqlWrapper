using System;
using System.Data;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SqlWrapper {
    public class SqlCommand : IMySqlRenderable{
        public SqlCommand() {
        }

        public MySqlCommand makeMySqlCommand(MySqlConnection conn, ERenderType renderType) {

            RenderContext renderContext = new RenderContext(renderType);

            string sqlDelete = this.render(renderContext);

            if (renderType == ERenderType.NonParamed) {

                return new MySqlCommand(sqlDelete, conn);
            } else {

                MySqlCommand cmd = new MySqlCommand(sqlDelete, conn);

                foreach (ParamPair pair in renderContext.paramPairs) {

                    cmd.Parameters.AddWithValue(pair.parameter, pair.userInput);
                }

                return cmd;
            }
        }

        public virtual string render(RenderContext renderContext) {
            throw new NotImplementedException();
        }
    }
}
