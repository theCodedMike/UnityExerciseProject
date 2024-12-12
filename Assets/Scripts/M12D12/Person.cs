using System.Collections.Generic;

namespace M12D12
{

    [System.Serializable]
    public class Person
    {
        public string name;
        public int age;
        public bool isStudent;

        public Person(string name, int age, bool isStudent)
        {
            this.name = name;
            this.age = age;
            this.isStudent = isStudent;
        }

        public override string ToString()
        {
            return $"name:{name}, age:{age}, isStudent:{isStudent}";
        }
    }

    [System.Serializable]
    public class Book
    {
        public string title;
        public Author author;
        public List<string> tags;
        public float rating;
        public bool isPublished;

        [System.NonSerialized] // 不序列化某个字段
        public string comments;
    }

    [System.Serializable]
    public class Author
    {
        public string name;
        public int age;
        public bool isVerified;
    }
}
