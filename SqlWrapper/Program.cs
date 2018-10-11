//using System;
//using System.Data;
//using System.Collections.ObjectModel;
//using System.Collections.Generic;
//using MySql.Data;
//using MySql.Data.MySqlClient;

//namespace SqlWrapper {

//    public class Assigmnets : DataTable {
//        public Assigmnets(string tableName) : base(tableName) {
//        }

//        public static string tableName = "Assignments";

//        public static COLUMN id = new COLUMN(Assigmnets.tableName, "Id");

//    }

//    public class ClassRooms : DataTable {
//        public ClassRooms(string tableName) : base(tableName) {
//        }

//        public static string tableName = "ClassRooms";
//        public static COLUMN id = new COLUMN(ClassRooms.tableName, "Id");
//    }


//    public static class Tables {

//        public static DataTable Assignments = new DataTable("Assignments");
//        public static DataTable ClassRooms = new DataTable("ClassRooms");

//    }




//    class MainClass {

//        class a : DataTable {
//            public a() : base(a.tableName) {
//            }


//            public static string tableName = "a";

//            public static COLUMN id = new COLUMN(a.tableName, "Id");
//        }

//        class c : DataTable {
//            public c() : base(c.tableName) {
//            }


//            public static string tableName = "c";

//            public static COLUMN id = new COLUMN(c.tableName, "Id");
//        }


//        public static void Main(string[] args) {


//            UPDATE update = new UPDATE(Tables.Assignments)
//                .addValuePair(
//                    Assigmnets.id,
//                    new IntLiteral(3))
//                .WHERE(
//                    new OperatorExpression(
//                        Assigmnets.id,
//                        new Operator(EOperatorType.Equals),
//                        new IntLiteral(2)

//                    )

//                );

//            DELETE delete = new DELETE()
//                .FROM(Tables.Assignments)
//                .WHERE(
//                    new OperatorExpression(
//                        Assigmnets.id,
//                        new Operator(EOperatorType.Equals),
//                        new StringLiteral("a")
//                    )
//                ) ;

//            string queryString = delete.render(ERenderType.Paramed);


           


//            Console.WriteLine(queryString);
//        }
//    }
//}
