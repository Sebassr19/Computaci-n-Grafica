using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO; // Necesario para trabajar con archivos
using System; // Para DateTime

public class Controller_Scene_3 : MonoBehaviour
{
    List<Student> list_students = new List<Student>();

    public TMP_InputField tnameS;
    public TMP_InputField tmailS;
    public TMP_InputField tageS;
    public TMP_InputField tcourseS;
    public TMP_InputField tcodeS;
    public TMP_Text displayText;

    private string jsonFileName = "studentsData.json";

    void Start()
    {
        // Cargar datos al iniciar
        // LoadStudentsFromJson();
    }

    public void AddStudent()
    {
        // Validaciones
        if (string.IsNullOrEmpty(tnameS.text) ||
            string.IsNullOrEmpty(tmailS.text) ||
            string.IsNullOrEmpty(tageS.text) ||
            string.IsNullOrEmpty(tcourseS.text) ||
            string.IsNullOrEmpty(tcodeS.text))
        {
            Debug.LogWarning("Por favor, complete todos los campos.");
            return;
        }

        int age;
        if (!int.TryParse(tageS.text, out age))
        {
            Debug.LogWarning("La edad debe ser un numero valido.");
            return;
        }

        // Crear y agregar estudiante
        Student student = new Student(tnameS.text, tmailS.text, age,
                                     tcourseS.text, tcodeS.text);
        list_students.Add(student);

        Debug.Log($"Estudiante agregado: {student.NameP}");
        ClearInputFields();

        // Guardar automaticamente despues de agregar
        // SaveStudentsToJson();
    }

    // Guardar lista en JSON
    public void SaveStudentsToJson()
    {
        if (list_students.Count == 0)
        {
            Debug.LogWarning("No hay estudiantes para guardar.");
            UpdateDisplay("No hay estudiantes para guardar.");
            return;
        }

        try
        {
            // Ruta donde se guardara el archivo
            string filePath = Path.Combine(Application.persistentDataPath, jsonFileName);

            // Convertir la lista a JSON
            string jsonData = JsonUtility.ToJson(new StudentListWrapper(list_students), true);

            // Escribir en el archivo
            File.WriteAllText(filePath, jsonData);

            Debug.Log($"Datos guardados en JSON\nRuta: {filePath}");
            Debug.Log($"Total de estudiantes guardados: {list_students.Count}");

            UpdateDisplay($"Datos guardados exitosamente\n" +
                         $"Archivo: {jsonFileName}\n" +
                         $"Estudiantes guardados: {list_students.Count}\n" +
                         $"Ubicacion: {filePath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error al guardar JSON: {e.Message}");
            UpdateDisplay($"Error al guardar: {e.Message}");
        }
    }

    // Cargar lista desde JSON
    public void LoadStudentsFromJson()
    {
        try
        {
            string filePath = Path.Combine(Application.persistentDataPath, jsonFileName);

            // Verificar si el archivo existe
            if (!File.Exists(filePath))
            {
                Debug.LogWarning("No existe archivo JSON para cargar.");
                UpdateDisplay("No se encontro archivo de datos guardados.");
                return;
            }

            // Leer el archivo JSON
            string jsonData = File.ReadAllText(filePath);

            // Convertir JSON a lista
            StudentListWrapper wrapper = JsonUtility.FromJson<StudentListWrapper>(jsonData);

            if (wrapper != null && wrapper.students != null)
            {
                list_students = wrapper.students;
                Debug.Log($"¡Datos cargados desde JSON!\nEstudiantes cargados: {list_students.Count}");

                UpdateDisplay($"Datos cargados exitosamente!\n" +
                             $"Estudiantes cargados: {list_students.Count}\n" +
                             "Usa 'Mostrar Estudiantes' para ver la lista.");
            }
            else
            {
                Debug.LogWarning("El archivo JSON está vacio o corrupto.");
                UpdateDisplay("El archivo de datos está vacio o corrupto.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error al cargar JSON: {e.Message}");
            UpdateDisplay($"Error al cargar: {e.Message}");
        }
    }

    // NUEVO MÉTODO: Ver información del archivo JSON
    public void ShowJsonInfo()
    {
        string filePath = Path.Combine(Application.persistentDataPath, jsonFileName);

        if (File.Exists(filePath))
        {
            FileInfo fileInfo = new FileInfo(filePath);
            string jsonData = File.ReadAllText(filePath);
            StudentListWrapper wrapper = JsonUtility.FromJson<StudentListWrapper>(jsonData);
            int studentCount = wrapper?.students?.Count ?? 0;

            string info = $"=== INFORMACION DEL ARCHIVO ===\n";
            info += $"Nombre: {jsonFileName}\n";
            info += $"Ubicacion: {filePath}\n";
            info += $"Tamaño: {fileInfo.Length} bytes\n";
            info += $"Última modificacion: {fileInfo.LastWriteTime}\n";
            info += $"Estudiantes en archivo: {studentCount}\n";
            info += $"Estudiantes en memoria: {list_students.Count}\n";

            UpdateDisplay(info);
        }
        else
        {
            UpdateDisplay("No existe archivo JSON guardado.\n" +
                         "Guarda datos primero usando 'Guardar en JSON'.");
        }
    }

    // Clase wrapper necesaria para serializar listas
    [Serializable]
    private class StudentListWrapper
    {
        public List<Student> students;

        public StudentListWrapper(List<Student> studentList)
        {
            this.students = studentList;
        }
    }

    // Método para mostrar estudiantes
    public void ShowAllStudents()
    {
        if (list_students.Count == 0)
        {
            UpdateDisplay("No hay estudiantes registrados.");
            return;
        }

        string displayContent = $"=== LISTA DE ESTUDIANTES ===\n";
        displayContent += $"Total: {list_students.Count} estudiante(s)\n\n";

        for (int i = 0; i < list_students.Count; i++)
        {
            Student student = list_students[i];
            displayContent += $"[{i + 1}] {student.NameP}\n";
            displayContent += $"   Email: {student.MailP} | Edad: {student.AgeP}\n";
            displayContent += $"   Curso: {student.CourseS} | Código: {student.CodeS}\n\n";
        }

        UpdateDisplay(displayContent);
    }

    // Método auxiliar para actualizar display
    private void UpdateDisplay(string message)
    {
        if (displayText != null)
            displayText.text = message;
    }

    private void ClearInputFields()
    {
        tnameS.text = "";
        tmailS.text = "";
        tageS.text = "";
        tcourseS.text = "";
        tcodeS.text = "";
        tnameS.Select();
    }

    // Método para limpiar la lista en memoria
    public void ClearStudentsList()
    {
        list_students.Clear();
        UpdateDisplay("Lista en memoria limpiada.\nEstudiantes actuales: 0");
    }
}