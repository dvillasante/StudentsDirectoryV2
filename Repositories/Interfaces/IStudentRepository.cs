using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task<bool> Create(Student student);

        Task<bool> Update(Student student);

        Task<bool> Delete(int id);
        Student Get(int search);

        List<Student> GetAll();
    }
}
