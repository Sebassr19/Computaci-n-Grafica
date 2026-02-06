using System;
using UnityEngine;

[System.Serializable]
public class Student : Person
{
    public string courseS;
    public string codeS;

    public Student()
    {
    }

    public Student(string nameP, string mailP, int ageP, string courseS, string codeS) : base(nameP, mailP, ageP)
    {
        this.courseS = courseS;
        this.codeS = codeS;
    }


    public string CourseS { get => courseS; set => courseS = value; }
    public string CodeS { get => codeS; set => codeS = value; }
}
