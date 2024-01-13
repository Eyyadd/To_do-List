using Microsoft.EntityFrameworkCore;
using System.Transactions;
using To_Do_List.Entities;

namespace To_Do_List
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var _context = new ApplicationDbContext();
            bool UserChoice;
            WrongChooseforWelcome:
            Console.WriteLine("------------------------ Welcome to the To_do List ------------------------");
            Console.WriteLine("1 - Login as existing user \t\t2- Register as new account");
            UserChoice = int.TryParse(Console.ReadLine().ToUpper(), out int Choice);
            if(UserChoice)
            {
                if(Choice==1)
                {
                    loginAgain:
                    Console.Clear();
                    Console.WriteLine("------------------------------- Login -------------------------------------");
                    Console.Write("Mail : ");
                    string mail = Console.ReadLine();
                    Console.Write("Password : ");
                    int password = int.Parse(Console.ReadLine()) ;
                    WrongChooseForOperation:
                    var mailCheck = _context.Users.Any(p => p.Mail == mail && p.Password == password);
                    if(mailCheck)
                    {
                        Console.Clear();
                        var retreive = _context.Users.SingleOrDefault(p => p.Mail == mail);
                        Console.WriteLine("---------------------------- Operations ----------------------------------");
                        Console.WriteLine("Welcome "+retreive.FirstName+".  What do you want ? ");
                        Console.WriteLine("1- Add new Task \t\t\t2- Remove Task");
                        Console.WriteLine("3- View list of all Tasks  \t\t4- Exit\n5- Back");
                        bool ChooseOperation = int.TryParse(Console.ReadLine(),out int operation);
                        if(ChooseOperation)
                        {
                            
                            if (operation == 1)
                            {
                                Console.Clear();
                                Console.WriteLine("---------------------------- New Task ---------------------------------");
                                Console.Write("Enter The Task Name - [ OR ] - Enter '00' to back for main menu\n");
                                string taskname = Console.ReadLine();
                                if(taskname=="00")
                                {
                                    Console.Clear();
                                    goto WrongChooseForOperation;
                                }
                                else
                                {
                                    Console.WriteLine("\nWrite the Description for the task : ");
                                    string description = Console.ReadLine();
                                    var UserId = _context.Users.FirstOrDefault(u => u.Mail == mail);
                                    _context.Tasks.Add(new Tasks { TaskName = taskname, Content = description, userId = UserId.userId });
                                    _context.SaveChanges();
                                backorExit:
                                    Console.WriteLine("Successfully added, Do you want back to the main menu ? (y\\n)");
                                    bool ChoiceForback = char.TryParse(Console.ReadLine().ToUpper(), out char backOrNot);
                                    if (ChoiceForback)
                                    {
                                        if (backOrNot == 'Y')
                                        {
                                            Thread.Sleep(500);
                                            Console.Clear();
                                            goto WrongChooseForOperation;
                                        }
                                        else if (backOrNot == 'N')
                                        {
                                            Thread.Sleep(500);
                                            Console.Clear();
                                            Console.WriteLine("Thank you .. GoodBye");

                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Wrong Choice , if you want to back for menu enter 'y' if you want to exit enter 'n'  ");
                                        Thread.Sleep(1000);
                                        Console.Clear();
                                        goto backorExit;
                                    }
                                }

                            }
                            else if (operation == 2)
                            {
                                Console.Clear();
                                backtoRemoveTask:
                                Console.WriteLine("----------------------------- Delete Task -----------------------------");
                                Console.WriteLine("Enter the Task Id you want to remove - [ OR ] - Enter '00' to back for main menu\n");
                                var UserId = _context.Users.FirstOrDefault(u => u.Mail == mail);
                                var allTasks = _context.Users.Join(_context.Tasks,
                                    U => U.userId,
                                    T => T.userId,
                                    (U, T) => new { U.userId, T.TaskId, T.TaskName, T.Content }
                                    ).Where(user => user.userId == UserId.userId);
                                Console.WriteLine(UserId.FirstName + " : ");
                                foreach (var Task in allTasks)
                                {
                                    Console.WriteLine("\t" + Task.TaskId + " - " + Task.TaskName);
                                    Console.WriteLine($"\t\t\t- {Task.Content}");
                                }
                                bool ChoiceTasknum = int.TryParse(Console.ReadLine(),out int taskid);
                                if(ChoiceTasknum)
                                {
                                    if(taskid==00)
                                    {
                                        Console.Clear();
                                        goto WrongChooseForOperation;
                                    }
                                    else
                                    {
                                        var CheckTaskifExist = _context.Tasks.Any(p => p.TaskId == taskid);
                                        if (CheckTaskifExist)
                                        {
                                            var DeletedUser = _context.Tasks.SingleOrDefault(p => p.TaskId == taskid);
                                            _context.Tasks.Remove(DeletedUser);
                                            _context.SaveChanges();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Wrong Task Number , Choose from the number behind the Tasks Names ");
                                            Thread.Sleep(1000);
                                            Console.Clear();
                                            goto backtoRemoveTask;

                                        }
                                    }
                                  
                                }
                                else
                                {
                                    Console.WriteLine("Wrong Task Number , Choose from the number behind the Tasks Names ");
                                    Thread.Sleep(1000);
                                    Console.Clear();
                                    goto backtoRemoveTask;
                                }
                              
                            backorExit:
                                Console.WriteLine("Successfully deleted, Do you want back to the main menu ? (y\\n)");
                                bool ChoiceForback = char.TryParse(Console.ReadLine().ToUpper(), out char backOrNot);
                                if (ChoiceForback)
                                {
                                    if (backOrNot == 'Y')
                                    {
                                        Thread.Sleep(500);
                                        Console.Clear();
                                        goto WrongChooseForOperation;
                                    }
                                    else if (backOrNot == 'N')
                                    {
                                        Thread.Sleep(500);
                                        Console.Clear();
                                        Console.WriteLine("Thank you .. GoodBye");

                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Wrong Choice , if you want to back for menu enter 'y' if you want to exit enter 'n'  ");
                                    Thread.Sleep(1000);
                                    Console.Clear();
                                    goto backorExit;
                                }

                            }
                            else if (operation == 3)
                            {
                                Console.Clear();
                                Console.WriteLine("----------------------------- Viwe Tasks -----------------------------\n");
                                var UserId = _context.Users.FirstOrDefault(u => u.Mail == mail);
                                var allTasks = _context.Users.Join(_context.Tasks,
                                    U => U.userId,
                                    T => T.userId,
                                    (U, T) => new { U.userId, T.TaskId, T.TaskName, T.Content }
                                    ).Where(user => user.userId == UserId.userId);
                                Console.WriteLine(UserId.FirstName + " : ");
                                foreach (var Task in allTasks)
                                {
                                    Console.WriteLine("\t" + Task.TaskId + " - " + Task.TaskName);
                                    Console.WriteLine($"\t\t\t- {Task.Content}");
                                }
                                _context.SaveChanges();
                            backorExit:
                                Console.WriteLine("\n Do you want back to the main menu ? (y\\n)");
                                bool ChoiceForback = char.TryParse(Console.ReadLine().ToUpper(), out char backOrNot);
                                if (ChoiceForback)
                                {
                                    if (backOrNot == 'Y')
                                    {
                                        Thread.Sleep(500);
                                        Console.Clear();
                                        goto WrongChooseForOperation;
                                    }
                                    else if (backOrNot == 'N')
                                    {
                                        Thread.Sleep(500);
                                        Console.Clear();
                                        Console.WriteLine("Thank you .. GoodBye");

                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Wrong Choice , if you want to back for menu enter 'y' if you want to exit enter 'n'  ");
                                    Thread.Sleep(1000);
                                    Console.Clear();
                                    goto backorExit;
                                }
                            }
                            else if (operation == 4)
                            {
                                
                                Console.WriteLine("Good Bye ");
                            }
                            else if (operation==5)
                            {
                                Console.Clear();
                                goto WrongChooseforWelcome;
                            }
                            else
                            {
                                Console.WriteLine("Wrong Operation Number .. Choose Only between 1 and 5 ");
                                Thread.Sleep(1500);
                                Console.Clear();
                                goto WrongChooseForOperation;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Wrong Operation Number .. Choose Only between 1 and 5 ");
                            Thread.Sleep(1500);
                            Console.Clear();
                            goto WrongChooseForOperation;
                        }

                    }
                    else
                    {
                        Console.WriteLine("Sorry.. Mail or Password incorrect ");
                        Thread.Sleep(1000);
                        goto loginAgain;
                    }
                }
                else if (Choice==2)
                {
                    Console.WriteLine("--------------------------- Register ---------------------------");
                    using var Trans= _context.Database.BeginTransaction();
                    try
                    {
                        Console.Write("First Name : ");
                        string fname = Console.ReadLine();
                        Console.Write("Last Name : ");
                        string lname = Console.ReadLine();
                        Console.Write("Age : ");
                        int age = int.Parse(Console.ReadLine());
                        Console.Write("Mail : ");
                        string mail = Console.ReadLine();
                        Console.Write("Password : ");
                        int password = int.Parse(Console.ReadLine());
                        _context.Users.Add(new User { FirstName = fname, LastName = lname, Age = age, Mail = mail, Password = password });
                        _context.SaveChanges();
                        Console.WriteLine("Sucessfully added User");
                        Trans.Commit();
                    }
                    catch(Exception e)
                    {
                        Trans.Rollback();
                        Console.Clear();
                        Console.WriteLine(e.InnerException);
                    }
                }
                else
                {
                    Console.WriteLine("Wrong Input, Choose numbers between 1 and 2 only.  ");
                    Thread.Sleep(1500);
                    Console.Clear();
                    goto WrongChooseforWelcome;
                }
            }
            else
            {
                Console.WriteLine("Wrong Input, Choose numbers between 1 and 2 only.  ");
                Thread.Sleep(1500);
                Console.Clear();
                goto WrongChooseforWelcome;
            }
        }
    }
}