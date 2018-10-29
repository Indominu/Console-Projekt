using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Etape_1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> myList = new List<string>();
            MyFunctions myfunctions = new MyFunctions();

            myList.Add("Ole");
            myList.Add("Peter");
            myList.Add("Jonna");
            myList.Add("Finn");
            myList.Add("Heidi");

            myfunctions.startMethod(myList);
        }

        class MyFunctions
        {
            public void startMethod(List<string> list)
            {
                bool quit = false;
                string command;
                string commandDo;
                string txt;

                while (quit == false)
                {
                    txt = Console.ReadLine();
                    list.RemoveAll(x => x == "");
                    string[] splitInput = txt.Trim().Split(new char[] { ' ' }, 2);
                    command = CleanText(splitInput[0]).ToLower();

                    if (splitInput.Length == 1)
                    {
                        if (command == "showall") { printList(list); }
                        else if (command == "quit") { quit = true; }
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
                                list.Add(commandDo);
                                break;
                            case "deleteperson":
                                bool isDeleted = false;
                                for (int i = 0; i < list.Count; i += 1)
                                {
                                    if (list[i] == commandDo)
                                    {
                                        list.Remove(commandDo);
                                        Console.WriteLine("Name was has been deleted");
                                        isDeleted = true;
                                        break;
                                    }
                                }
                                if (isDeleted == false) { Console.WriteLine("Name is not on the list"); }
                                break;
                            case "changeperson":
                                int nr = 0;
                                bool matcthes = false;
                                splitInput = commandDo.Trim().Split(new char[] { ' ' }, 2);
                                for (int i = 0; i < list.Count; i += 1)
                                {
                                    if (list[i] == splitInput[0])
                                    {
                                        nr = list.IndexOf(splitInput[0]);
                                        list.Remove(splitInput[0]);
                                        list.Insert(nr, splitInput[1]);
                                        Console.WriteLine("Name has been changed");
                                        matcthes = true;
                                        break;
                                    }
                                }
                                if (matcthes == false) { Console.WriteLine("Name is not on the list"); }
                                break;
                            default:
                                Console.WriteLine("Not a command");
                                break;
                        }
                    }
                }
            }

            public void printList(List<string> list)
            {
                for (int i = 0; i < list.Count; i += 1)
                    Console.WriteLine(list[i]);
            }

            public string CleanText(string text)
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
        }
    }
}
