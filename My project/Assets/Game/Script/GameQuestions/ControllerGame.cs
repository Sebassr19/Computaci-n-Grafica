using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ControllerGame : MonoBehaviour
{
    List<MultipleQuestion> multipleQuestions = new List<MultipleQuestion>();

    public TextMeshProUGUI questionText;        
    public TextMeshProUGUI option1Text;         
    public TextMeshProUGUI option2Text;         
    public TextMeshProUGUI option3Text;        
    public TextMeshProUGUI option4Text;       
    public TextMeshProUGUI verseText;          
    public TextMeshProUGUI difficultyText;    

    private int currentQuestionIndex = 0;

    void Start()
    {
        LoadMultipleQuestions();

        
        if (multipleQuestions.Count > 0)
        {
            ShowCurrentQuestion();
        }
    }

    void Update()
    {

    }

    public void LoadMultipleQuestions()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "ArchivoPreguntasMV3 - copia.txt");

        string[] allLines = File.ReadAllLines(filePath);

        foreach (string line in allLines)
        {

            string[] parts = line.Split('-');

            if (parts.Length >= 8)
            {
                MultipleQuestion question = new MultipleQuestion(
                    question: parts[0].Trim(),       
                    option1: parts[1].Trim(),      
                    option2: parts[2].Trim(),        
                    option3: parts[3].Trim(),         
                    option4: parts[4].Trim(),         
                    answer: parts[5].Trim(),        
                    versiculo: parts[6].Trim(),        
                    dificultty: parts[7].Trim()       
                );

                multipleQuestions.Add(question);
            }
            else
            {
            }
        }

    }

    public void ShowCurrentQuestion()
    {
        if (multipleQuestions.Count == 0)
        {
            return;
        }

        if (currentQuestionIndex < 0 || currentQuestionIndex >= multipleQuestions.Count)
        {
            return;
        }

        MultipleQuestion currentQuestion = multipleQuestions[currentQuestionIndex];

        if (questionText == null)
        {
            return;
        }

        questionText.text = currentQuestion.Question;
        option1Text.text = "A) " + currentQuestion.Option1;
        option2Text.text = "B) " + currentQuestion.Option2;
        option3Text.text = "C) " + currentQuestion.Option3;
        option4Text.text = "D) " + currentQuestion.Option4;
        verseText.text = "Versículo: " + currentQuestion.Versiculo;
        difficultyText.text = "Dificultad: " + currentQuestion.Dificultty;

    }
}
