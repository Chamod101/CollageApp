namespace CollageApp.Models
{
    public static class StudentsRepository
    {
        public static List<Student> Students { get; set; } = new List<Student>(){
                new Student
                {
                    Id = 1,
                    StudentName = "Chamod",
                    Email ="",
                    Address =""
                },
                new Student
                {
                    Id = 2,
                    StudentName = "Dilushanka",
                    Email ="",
                    Address =""
                },
                new Student
                {
                    Id = 3,
                    StudentName = "Perera",
                    Email ="",
                    Address =""
                },
            };
    }
}
