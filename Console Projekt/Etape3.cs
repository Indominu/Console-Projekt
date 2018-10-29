using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Etape_3
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
                List<string> fileList = OpenFileList(pathToFile);
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
                        if (command == "showall") { PrintList(fileList); }
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
                                fileList.Add(CleanText(commandDo));
                                Console.WriteLine("Name was has been added");
                                saveFile.SaveContent(pathToFile, fileList);
                                break;
                            case "deleteperson":
                                bool isDeleted = false;
                                for (int i = 0; i < fileList.Count; i += 1)
                                {
                                    if (fileList[i] == commandDo)
                                    {
                                        fileList.Remove(CleanText(commandDo));
                                        Console.WriteLine("Name was has been deleted");
                                        saveFile.SaveContent(pathToFile, fileList);
                                        isDeleted = true;
                                        break;
                                    }
                                }
                                if (isDeleted == false) { Console.WriteLine("Name is not on the list"); }
                                break;
                            case "changeperson":
                                bool matcthes = false;
                                splitInput = commandDo.Split(new char[] { ' ' }, 2);
                                for (int i = 0; i < fileList.Count; i += 1)
                                {
                                    if (fileList[i] == splitInput[0])
                                    {
                                        int nr = fileList.IndexOf(splitInput[0]);
                                        fileList.Remove(splitInput[0]);
                                        fileList.Insert(nr, splitInput[1]);
                                        Console.WriteLine("Name has been changed");
                                        saveFile.SaveContent(pathToFile, fileList);
                                        matcthes = true;
                                        break;
                                    }
                                }
                                if (matcthes == false) { Console.WriteLine("Name is not on the list"); }
                                break;
                            case "changeage":
                                saveFile.SaveContent(pathToFile, fileList);
                                break;
                            case "changebalance":
                                saveFile.SaveContent(pathToFile, fileList);
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

                for (int i = 0; i < splitInput.Length; i+=1)
                {
                    openFileList.Add(splitInput[i]);
                }

                openFileList.RemoveAll(x => x == "");

                return openFileList;
            }

            /// <summary>
            /// Shows the entire list on the console
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
                public void SaveContent(string pathToFile, List<string> data)
                {
                    string textToFile = "";

                    for (int i = 0; i < data.Count; i+=1)
                    {
                        textToFile = textToFile + " " + data[i];
                    }
                    {
                        File.WriteAllText(pathToFile, CleanText(textToFile));
                    }
                }
            }
        }
    }
}