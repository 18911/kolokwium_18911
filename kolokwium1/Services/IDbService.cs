using kolokwium1.Models;
using kolokwium1.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolokwium1.Services{
    public interface IDbService{
        public ProjectTasks GetTask(int id);

        public void AddTaskToDb(AddTask addTask);
    }
}
