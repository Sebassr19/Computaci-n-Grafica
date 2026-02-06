using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Controller_Scene_3 : MonoBehaviour
{

    List<Student> list_students = new List<Student>();
    public TMP_InputField tnameS;
    public TMP_InputField tmailS;
    public TMP_InputField tageS;
    public TMP_InputField tcourseS;
    public TMP_InputField tcodeS;
    public TMP_Text panelText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddStudent()
    {
        Student student = new Student(
            tnameS.text,
            tmailS.text,
            int.Parse(tageS.text),
            tcourseS.text,
            tcodeS.text
        );

        list_students.Add(student);

        ShowStudents();

        void ShowStudents()
        {
            panelText.text = ""; // Limpia el texto antes de escribir

            foreach (Student s in list_students)
            {
                panelText.text +=
                    "Nombre: " + s.NameP +
                    "\nCorreo: " + s.MailP +
                    "\nEdad: " + s.AgeP +
                    "\nCurso: " + s.CourseS +
                    "\nCódigo: " + s.CodeS +
                    "\n----------------------\n";
            }
        }
        Student student = new Student(tnameS.text, tmailS.text, int.Parse(tageS.text), tcourseS.text, tcodeS.text);
        list_students.Add(student);
        Debug.Log("Student Added: " + student.NameP + ", " + student.MailP + ", " + student.AgeP + ", " + student.CourseS + ", " + student.CodeS);
    }
} 

