using UnityEngine;

public class Person
{
    private string nameP;
    private string mailP;
    private int ageP;

    public Person()
    {
    }

    public Person(string nameP, string mailP, int ageP)
    {
        this.nameP = nameP;
        this.mailP = mailP;
        this.ageP = ageP;
    }

    public string NameP { get => nameP; set => nameP = value; }
    public string MailP { get => mailP; set => mailP = value; }
    public int AgeP { get => ageP; set => ageP = value; }
}