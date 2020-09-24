using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IStudentsManager
    {
        Task<Student> Create(Student student);

        Task<Student> Update(Student student);

        Task<bool> Delete(int id);

        Student Get(int id);
        List<Student> GetAll();

        void LoadTestData();

    }
}
