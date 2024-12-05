using TennisApp.Data.Models;
using TennisApp.Models;

namespace TennisApp.Services.Interfaces
{
    public interface ILessonService
    {
        void AddLessonAsync(AddLessonViewModel lesson);
        void GetAllLessonsAsync();
        void GetLessonByIdAsync(int id);
        void DeleteLessonAsync(Lesson Lesson);
    }
}
