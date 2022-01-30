// See https://aka.ms/new-console-template for more information
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

string file = "Files/tickets.csv";
bool more = true;

do
{
    int entry = 0;
    PrintMenu();
    entry = Convert.ToInt32(Console.ReadLine());

    switch (entry)
    {
        case 1:
            ReadTickets(file);
            break;
        case 2:
            CreateTicket(file);
            break;
        case 3:
            more = false;
            break;
        default:
            Console.WriteLine("Invalid Entry!");
            break;
    }
} while (more);



static void PrintMenu()
{
    Console.WriteLine();
    Console.WriteLine("Ticketing System\n" +
                      "1. Read Tickets\n" +
                      "2. Create New Ticket\n" +
                      "3. Exit\n");
    Console.Write("Select an option: ");
}

static void ReadTickets(string file)
{
    if (File.Exists(file))
    {
        StreamReader sr = new StreamReader(file);
        int lineNumber = 1;

        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            if (lineNumber > 1)
            {
                string[] ticket = line.Split(",");
                Console.WriteLine(
                    "TicketID: {0}, Summary: {1}, Status: {2}, Priority: {3}, Submitter: {4}, Assigned: {5}, Watching: {6}",
                    ticket[0],ticket[1],ticket[2],ticket[3],ticket[4],ticket[5],ticket[6]);
            }

            lineNumber++;
        }

        Console.WriteLine("End of Tickets.");
        sr.Close();

    }
    else
    {
        Console.WriteLine("File does not exist!");
    }
}

static void CreateTicket(string file)
{
    if (File.Exists(file))
    {
        int newTicketCount = CountTickets(file) + 1;
        StreamWriter sw = new StreamWriter(file, true);

        string ticket = newTicketCount.ToString() + ",";

        Console.Write("Enter ticket summary: ");
        ticket = ticket + Console.ReadLine() + ",";
        
        Console.Write("Enter ticket status: ");
        ticket = ticket + Console.ReadLine() + ",";
        
        Console.Write("Enter ticket priority: ");
        ticket = ticket + Console.ReadLine() + ",";
        
        Console.Write("Enter ticket submitter: ");
        ticket = ticket + Console.ReadLine() + ",";
        
        Console.Write("Enter ticket asignee: ");
        ticket = ticket + Console.ReadLine() + ",";

        bool moreWatchers = true;
        do
        {
            Console.Write("Enter ticket watchers (enter 'done' when finished): ");
            string watcher = Console.ReadLine();
            if (watcher != "done")
            {
                ticket = ticket + watcher + "|"; 
            }
            else
            {
                moreWatchers = false;
            }
        } while (moreWatchers);

        sw.WriteLine();
        sw.WriteLine(ticket.Remove(ticket.Length - 1,1));
        sw.Close();

    }
    else
    {
        Console.WriteLine("File does not exist!");
    }
}

static int CountTickets(string file) 
{
    StreamReader sr = new StreamReader(file);
    int count = 0;
    while (!sr.EndOfStream)
    {
        string line = sr.ReadLine();
        count++;
    }

    sr.Close();
    return count - 1;
}