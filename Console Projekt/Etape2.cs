using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Etape_2
{
    class Program
    {
        static void Main(string[] args)
        {
            MyNewMainFunction newMain = new MyNewMainFunction();
            newMain.StartMethod();
        }

        class MyNewMainFunction
        {
            public void StartMethod()
            {
                SaveToFile saveFile = new SaveToFile();
                string pathToFile = @"D:\test.txt";
                bool isqutting = false;
                string command;
                string commandDo;
                string txt;

                while (isqutting == false)
                {
                    txt = Console.ReadLine();                                                   
                    string[] splitInput = txt.Trim().Split(new char[] { ' ' }, 2);
                    command = CleanText(splitInput[0]).ToLower();

                    if (splitInput.Length == 1)
                    {
                        if (command == "showall") { PrintList(OpenFileList(pathToFile)); }
                        else if (command == "quit") { isqutting = true; }
                        else if (command == "") { Console.WriteLine("You entered nothing"); }
                        else { Console.WriteLine("Need more for this command"); }
                    }
                    else
                    {
                        commandDo = CleanText(splitInput[1]);

                        switch (command)
                        {
                            case "addperson":
                                Console.WriteLine("Name was has been added");
                                saveFile.SaveContent(pathToFile, commandDo, command);
                                break;
                            case "deleteperson":
                                bool isDeleted = false;
                                for (int i = 0; i < OpenFileList(pathToFile).Count; i += 1)
                                {
                                    if (OpenFileList(pathToFile)[i] == commandDo)
                                    {
                                        saveFile.SaveContent(pathToFile, commandDo, command);
                                        Console.WriteLine("Name was has been deleted");
                                        isDeleted = true;
                                        break;
                                    }
                                }
                                if (isDeleted == false) { Console.WriteLine("Name is not on the list"); }
                                break;
                            case "changeperson":
                                bool matcthes = false;
                                splitInput = commandDo.Trim().Split(new char[] { ' ' }, 2);
                                for (int i = 0; i < OpenFileList(pathToFile).Count; i += 1)
                                {
                                    if (OpenFileList(pathToFile)[i] == splitInput[0])
                                    {
                                        saveFile.SaveContent(pathToFile, splitInput[0] + " " + splitInput[1], command);
                                        Console.WriteLine("Name has been changed");
                                        matcthes = true;
                                        break;
                                    }
                                }
                                if (matcthes == false) { Console.WriteLine("Name is not on the list"); }
                                break;
                            case "changeage":
                                saveFile.SaveContent(pathToFile, commandDo, command);
                                break;
                            default:
                                Console.WriteLine("Not a command");
                                break;
                        }
                    }
                }
            }

            public List<string> OpenFileList(string pathToFile)
            {
                List<string> openFileList = new List<string>();

                string textFromFile = CleanText(File.ReadAllText(pathToFile));
                string[] splitInput = textFromFile.Trim().Split(new char[] { ' ' });

                for (int i = 0; i < splitInput.Length; i+=3)
                {
                    openFileList.Add(splitInput[i] + " " + splitInput[i+1] + " " + splitInput[i+2]);
                }

                openFileList.RemoveAll(x => x == "  ");

                return openFileList;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="list"></param>
            public void PrintList(List<string> list)
            {
                for (int i = 0; i < list.Count; i += 1)
                    Console.WriteLine(list[i]);
            }

            /// <summary>
            /// Removes all space at the sides of a string of text 
            /// and all space between words gets reduced to one
            /// </summary>
            /// <param name="text"></param>
            /// <returns></returns>
            public static string CleanText(string text)
            {
                string oldText = text.Trim();
                string newText;

                do
                {
                    newText = oldText;
                    oldText = newText.Replace("  ", " ");
                }
                while (newText != oldText);

                return newText;
            }

            class SaveToFile
            {
                public void SaveContent(string pathToFile, string data, string command)
                {
                    string textInFile;
                    string[] splitInput;
                    int indexOfPerson;

                    if (command == "addperson") { File.AppendAllText(pathToFile, data + " "); }
                    if (command == "deleteperson")
                    {
                        textInFile = CleanText(File.ReadAllText(pathToFile).Replace(data, ""));
                        File.WriteAllText(pathToFile, textInFile);
                    }
                    if (command == "changeperson")
                    {
                        splitInput = data.Trim().Split(new char[] { ' ' }, 2);
                        textInFile = File.ReadAllText(pathToFile).Replace(splitInput[0], splitInput[1]);
                        File.WriteAllText(pathToFile, textInFile);
                    }
                    if (command == "changeage")
                    {
                        splitInput = data.Trim().Split(new char[] { ' ' }, 2);
                        textInFile = File.ReadAllText(pathToFile);
                        indexOfPerson = textInFile.IndexOf(splitInput[0]);
                        textInFile = textInFile.Insert(indexOfPerson+1, splitInput[1]);
                        File.WriteAllText(pathToFile, textInFile);
                    }
                }
            }
        }
    }
}