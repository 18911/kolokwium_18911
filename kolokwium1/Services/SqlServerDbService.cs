using kolokwium1.Models;
using kolokwium1.Request;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace kolokwium1.Services{
    public class SqlServerDbService : IDbService{
        private const string Conncetion = "Data Source=db-mssql;Initial Catalog=s18911;Integrated Security=True;MultipleActiveResultSets=True";


        public void AddTaskToDb(AddTask addTask) {
            int taskTypeId = 0;
            try{
                using (SqlConnection conn = new SqlConnection(Conncetion))
                using (SqlCommand command1 = new SqlCommand()){
                    if (!(addTask.taskType == null))
                    {
                        command1.Connection = conn;
                        command1.CommandText = "select IdTaskType from TaskType where IdTaskType = @id";
                        command1.Parameters.AddWithValue("id", addTask.taskType.IdTaskType);

                        conn.Open();
                        var sqlReader = command1.ExecuteReader();

                        if (!sqlReader.Read())
                        {
                            using (SqlCommand command2 = new SqlCommand())
                            {
                                command2.Connection = conn;
                                command2.CommandText = "select max(IdTaskType)+1 from TaskType";

                                sqlReader = command2.ExecuteReader();
                                if (sqlReader.Read())
                                {
                                    taskTypeId = Int32.Parse(sqlReader[0].ToString());
                                }
                            }

                            using (SqlCommand command3 = new SqlCommand())
                            {
                                command3.Connection = conn;
                                command3.CommandText = "insert into TaskType(IdTaskType,Name) values(id,name)";
                                command3.Parameters.Add(new SqlParameter("id", taskTypeId));
                                command3.Parameters.Add(new SqlParameter("id", addTask.taskType.Name));

                                command3.ExecuteNonQuery();

                            }
                        }

                        int idTaskDb = 0;

                        using (SqlCommand command4 = new SqlCommand())
                        {

                            command4.Connection = conn;
                            command4.CommandText = "select max(IdTaskType)+1 from TaskType";

                            sqlReader = command4.ExecuteReader();
                            if (sqlReader.Read())
                            {
                                idTaskDb = Int32.Parse(sqlReader[0].ToString());
                            }

                        }

                        using (SqlCommand command5 = new SqlCommand())
                        {
                            command5.Connection = conn;
                            command5.CommandText = "insert into Task values(IdTask,Name,Description,Deadline,IdTeam,IdTaskType,IdAssignedTo,IdCreator)";

                            command5.Parameters.Add(new SqlParameter("IdTask", idTaskDb));
                            command5.Parameters.Add(new SqlParameter("Name", addTask.Name));
                            command5.Parameters.Add(new SqlParameter("Description", addTask.Description));
                            command5.Parameters.Add(new SqlParameter("Description", addTask.Deadline));
                            command5.Parameters.Add(new SqlParameter("Description", addTask.IdTeam));
                            command5.Parameters.Add(new SqlParameter("Description", taskTypeId));
                            command5.Parameters.Add(new SqlParameter("Description", addTask.IdAssignedTo));
                            command5.Parameters.Add(new SqlParameter("Description", addTask.IdCreator));

                            command5.ExecuteNonQuery();
                        }

                        sqlReader.Close();
                    }
                    else {
                        int idTaskDb = 0;

                        using (SqlCommand command4 = new SqlCommand()){

                            command4.Connection = conn;
                            command4.CommandText = "select max(IdTaskType)+1 from TaskType";

                            var sqlReader = command4.ExecuteReader();
                            if (sqlReader.Read()){
                                idTaskDb = Int32.Parse(sqlReader[0].ToString());
                            }

                            using (SqlCommand command5 = new SqlCommand()){
                                command5.Connection = conn;
                                command5.CommandText = "insert into Task values(IdTask,Name,Description,Deadline,IdTeam,IdTaskType,IdAssignedTo,IdCreator)";

                                command5.Parameters.Add(new SqlParameter("IdTask", idTaskDb));
                                command5.Parameters.Add(new SqlParameter("Name", addTask.Name));
                                command5.Parameters.Add(new SqlParameter("Description", addTask.Description));
                                command5.Parameters.Add(new SqlParameter("Description", addTask.Deadline));
                                command5.Parameters.Add(new SqlParameter("Description", addTask.IdTeam));
                                command5.Parameters.Add(new SqlParameter("Description", null));
                                command5.Parameters.Add(new SqlParameter("Description", addTask.IdAssignedTo));
                                command5.Parameters.Add(new SqlParameter("Description", addTask.IdCreator));

                                command5.ExecuteNonQuery();
                            }

                            sqlReader.Close();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public ProjectTasks GetTask(int id) {
            ProjectTasks projectTasks = new ProjectTasks();
            try{
                using (SqlConnection conn = new SqlConnection(Conncetion))
                using (SqlCommand command1 = new SqlCommand())
                {
                    command1.Connection = conn;
                    command1.CommandText = "select IdTask,Task.Name,Description,Deadline,TaskType.Name from Project inner join Task on Project,IdTeam = Task.IdTeam " +
                        "inner join TakType on Task.IdTaskType = TaskType.IdTaskType where Project.IdTeam = @id orderBy Task.Deadline DESC";

                    command1.Parameters.AddWithValue("id", id);

                    conn.Open();
                    var sqlReader = command1.ExecuteReader();

                    Models.Task taskCol;

                    while (sqlReader.Read())
                    {
                        taskCol = new Models.Task();

                        taskCol.idTask = Int32.Parse(sqlReader["IdTask"].ToString());
                        taskCol.Name = sqlReader["Task.Name"].ToString();
                        taskCol.Description = sqlReader["Description"].ToString();
                        taskCol.Deadline = DateTime.Parse(sqlReader["Deadline"].ToString());
                        taskCol.taskType.Name = sqlReader["TaskType.Name"].ToString();

                        projectTasks.listTask.Add(taskCol);
                    }
                    sqlReader.Close();
                }
            }
            catch (Exception ex){
                Console.WriteLine(ex);
            }
            return projectTasks;
        }
    }
}
