namespace TicketProject;

class Program
{
    static void Main(string[] args)
        {
            Console.WriteLine("=== Ticket Management System ===\n");
            
            // Initialize the system
            TicketSystem ticketSystem = new TicketSystem();
            SeedData(ticketSystem);
            
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nTicket Management System Menu:");
                Console.WriteLine("1. View All Tickets");
                Console.WriteLine("2. View Ticket Details");
                Console.WriteLine("3. Create New Ticket");
                Console.WriteLine("4. Assign Ticket");
                Console.WriteLine("5. Update Ticket Status");
                Console.WriteLine("6. Add Comment to Ticket");
                Console.WriteLine("7. View Dashboard");
                Console.WriteLine("8. Search Tickets");
                Console.WriteLine("9. Export Tickets");
                Console.WriteLine("0. Exit");
                
                Console.Write("\nEnter your choice: ");
                string choice = Console.ReadLine();
                
                try
                {
                    switch (choice)
                    {
                        case "1":
                            ViewAllTickets(ticketSystem);
                            break;
                        case "2":
                            ViewTicketDetails(ticketSystem);
                            break;
                        case "3":
                            CreateNewTicket(ticketSystem);
                            break;
                        case "4":
                            AssignTicket(ticketSystem);
                            break;
                        case "5":
                            UpdateTicketStatus(ticketSystem);
                            break;
                        case "6":
                            AddCommentToTicket(ticketSystem);
                            break;
                        case "7":
                            ViewDashboard(ticketSystem);
                            break;
                        case "8":
                            SearchTickets(ticketSystem);
                            break;
                        case "9":
                            ExportTickets(ticketSystem);
                            break;
                        case "0":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            
            Console.WriteLine("\nThank you for using the Ticket Management System!");
        }
        
        #region Menu Actions
        
        static void ViewAllTickets(TicketSystem ticketSystem)
        {
            Console.WriteLine("\n=== All Tickets ===");
            
            var tickets = ticketSystem.GetAllTickets();
            if (tickets.Count == 0)
            {
                Console.WriteLine("No tickets found.");
                return;
            }
            
            DisplayTicketList(tickets);
        }
        
        static void ViewTicketDetails(TicketSystem ticketSystem)
        {
            Console.Write("\nEnter ticket ID: ");
            if (!int.TryParse(Console.ReadLine(), out int ticketId))
            {
                Console.WriteLine("Invalid ticket ID.");
                return;
            }
            
            var ticket = ticketSystem.GetTicketById(ticketId);
            if (ticket == null)
            {
                Console.WriteLine("Ticket not found.");
                return;
            }
            
            DisplayTicketDetails(ticket);
        }
        
        static void CreateNewTicket(TicketSystem ticketSystem)
        {
            Console.WriteLine("\n=== Create New Ticket ===");
            
            Console.Write("Title: ");
            string title = Console.ReadLine();
            
            Console.Write("Description: ");
            string description = Console.ReadLine();
            
            Console.WriteLine("Select Priority:");
            Console.WriteLine("1. Low");
            Console.WriteLine("2. Medium");
            Console.WriteLine("3. High");
            Console.WriteLine("4. Critical");
            Console.Write("Choice: ");
            
            if (!Enum.TryParse(Console.ReadLine(), out Priority priority))
                priority = Priority.Medium;
            
            Console.WriteLine("Select Category:");
            int index = 1;
            foreach (var category1 in Enum.GetValues<TicketCategory>())
            {
                Console.WriteLine($"{index++}. {category1}");
            }
            Console.Write("Choice: ");
            
            if (!Enum.TryParse(Console.ReadLine(), out TicketCategory category))
                category = TicketCategory.Other;
            
            Console.Write("Reporter Name: ");
            string reporter = Console.ReadLine();
            
            Ticket ticket = new Ticket
            {
                Title = title,
                Description = description,
                Priority = priority,
                Category = category,
                ReportedBy = reporter
            };
            
            ticketSystem.AddTicket(ticket);
            Console.WriteLine($"Ticket created with ID: {ticket.Id}");
        }
        
        static void AssignTicket(TicketSystem ticketSystem)
        {
            Console.WriteLine("\n=== Assign Ticket ===");
            
            Console.Write("Enter ticket ID: ");
            if (!int.TryParse(Console.ReadLine(), out int ticketId))
            {
                Console.WriteLine("Invalid ticket ID.");
                return;
            }
            
            var ticket = ticketSystem.GetTicketById(ticketId);
            if (ticket == null)
            {
                Console.WriteLine("Ticket not found.");
                return;
            }
            
            Console.WriteLine("Available team members:");
            var team = ticketSystem.GetTeamMembers();
            for (int i = 0; i < team.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {team[i].Name} ({team[i].Role})");
            }
            
            Console.Write("Choose team member (number): ");
            if (!int.TryParse(Console.ReadLine(), out int memberIndex) || memberIndex < 1 || memberIndex > team.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }
            
            var member = team[memberIndex - 1];
            ticketSystem.AssignTicket(ticket.Id, member);
            Console.WriteLine($"Ticket #{ticket.Id} assigned to {member.Name}");
        }
        
        static void UpdateTicketStatus(TicketSystem ticketSystem)
        {
            Console.WriteLine("\n=== Update Ticket Status ===");
            
            Console.Write("Enter ticket ID: ");
            if (!int.TryParse(Console.ReadLine(), out int ticketId))
            {
                Console.WriteLine("Invalid ticket ID.");
                return;
            }
            
            var ticket = ticketSystem.GetTicketById(ticketId);
            if (ticket == null)
            {
                Console.WriteLine("Ticket not found.");
                return;
            }
            
            Console.WriteLine("Current status: " + ticket.Status);
            Console.WriteLine("Select new status:");
            int index = 1;
            foreach (var status in Enum.GetValues<TicketStatus>())
            {
                Console.WriteLine($"{index++}. {status}");
            }
            
            Console.Write("Choice: ");
            if (!int.TryParse(Console.ReadLine(), out int statusIndex) || statusIndex < 1 || statusIndex > Enum.GetValues<TicketStatus>().Length)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }
            
            TicketStatus newStatus = Enum.GetValues<TicketStatus>()[statusIndex - 1];
            ticketSystem.UpdateTicketStatus(ticket.Id, newStatus);
            Console.WriteLine($"Ticket #{ticket.Id} status updated to {newStatus}");
        }
        
        static void AddCommentToTicket(TicketSystem ticketSystem)
        {
            Console.WriteLine("\n=== Add Comment to Ticket ===");
            
            Console.Write("Enter ticket ID: ");
            if (!int.TryParse(Console.ReadLine(), out int ticketId))
            {
                Console.WriteLine("Invalid ticket ID.");
                return;
            }
            
            var ticket = ticketSystem.GetTicketById(ticketId);
            if (ticket == null)
            {
                Console.WriteLine("Ticket not found.");
                return;
            }
            
            Console.Write("Your name: ");
            string author = Console.ReadLine();
            
            Console.Write("Comment: ");
            string text = Console.ReadLine();
            
            ticketSystem.AddCommentToTicket(ticket.Id, author, text);
            Console.WriteLine("Comment added successfully.");
        }
        
        static void ViewDashboard(TicketSystem ticketSystem)
        {
            Console.WriteLine("\n=== Dashboard ===");
            
            var stats = ticketSystem.GetDashboardStats();
            
            Console.WriteLine($"Total Tickets: {stats.TotalTickets}");
            Console.WriteLine($"Open Tickets: {stats.OpenTickets}");
            Console.WriteLine($"In Progress Tickets: {stats.InProgressTickets}");
            Console.WriteLine($"Resolved Tickets: {stats.ResolvedTickets}");
            Console.WriteLine($"Closed Tickets: {stats.ClosedTickets}");
            
            Console.WriteLine("\nTickets by Priority:");
            foreach (var kvp in stats.TicketsByPriority)
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
            }
            
            Console.WriteLine("\nTickets by Category:");
            foreach (var kvp in stats.TicketsByCategory)
            {
                Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
            }
            
            Console.WriteLine("\nTop 5 Recent Tickets:");
            DisplayTicketList(stats.RecentTickets);
        }
        
        static void SearchTickets(TicketSystem ticketSystem)
        {
            Console.WriteLine("\n=== Search Tickets ===");
            
            Console.WriteLine("Search by:");
            Console.WriteLine("1. Keyword (Title/Description)");
            Console.WriteLine("2. Status");
            Console.WriteLine("3. Priority");
            Console.WriteLine("4. Assignee");
            Console.Write("Choice: ");
            
            string searchChoice = Console.ReadLine();
            List<Ticket> results = new List<Ticket>();
            
            switch (searchChoice)
            {
                case "1":
                    Console.Write("Enter keyword: ");
                    string keyword = Console.ReadLine();
                    results = ticketSystem.SearchTickets(keyword);
                    break;
                    
                case "2":
                    Console.WriteLine("Select status:");
                    int index = 1;
                    foreach (var status in Enum.GetValues<TicketStatus>())
                    {
                        Console.WriteLine($"{index++}. {status}");
                    }
                    Console.Write("Choice: ");
                    if (int.TryParse(Console.ReadLine(), out int statusIndex) && 
                        statusIndex >= 1 && statusIndex <= Enum.GetValues<TicketStatus>().Length)
                    {
                        TicketStatus status = Enum.GetValues<TicketStatus>()[statusIndex - 1];
                        results = ticketSystem.GetTicketsByStatus(status);
                    }
                    break;
                    
                case "3":
                    Console.WriteLine("Select priority:");
                    Console.WriteLine("1. Low");
                    Console.WriteLine("2. Medium");
                    Console.WriteLine("3. High");
                    Console.WriteLine("4. Critical");
                    Console.Write("Choice: ");
                    if (int.TryParse(Console.ReadLine(), out int priorityIndex) &&
                        priorityIndex >= 1 && priorityIndex <= Enum.GetValues<Priority>().Length)
                    {
                        Priority priority = Enum.GetValues<Priority>()[priorityIndex - 1];
                        results = ticketSystem.GetTicketsByPriority(priority);
                    }
                    break;
                    
                case "4":
                    Console.Write("Enter assignee name: ");
                    string assigneeName = Console.ReadLine();
                    results = ticketSystem.GetTicketsByAssignee(assigneeName);
                    break;
                    
                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }
            
            if (results.Count == 0)
            {
                Console.WriteLine("No tickets found matching your criteria.");
                return;
            }
            
            Console.WriteLine($"\nFound {results.Count} ticket(s):");
            DisplayTicketList(results);
        }
        
        static void ExportTickets(TicketSystem ticketSystem)
        {
            Console.WriteLine("\n=== Export Tickets ===");
            
            Console.WriteLine("Export format:");
            Console.WriteLine("1. JSON");
            Console.WriteLine("2. CSV");
            Console.Write("Choice: ");
            
            string choice = Console.ReadLine();
            ITicketExporter exporter;
            string fileName;
            
            switch (choice)
            {
                case "1":
                    exporter = new JsonTicketExporter();
                    fileName = "tickets_export.json";
                    break;
                case "2":
                    exporter = new CsvTicketExporter();
                    fileName = "tickets_export.csv";
                    break;
                default:
                    Console.WriteLine("Invalid choice. Using JSON format.");
                    exporter = new JsonTicketExporter();
                    fileName = "tickets_export.json";
                    break;
            }
            
            try
            {
                string result = ticketSystem.ExportTickets(exporter);
                File.WriteAllText(fileName, result);
                Console.WriteLine($"Tickets exported successfully to {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error exporting tickets: {ex.Message}");
            }
        }
        
        #endregion
        
        #region Helper Methods
        
        static void DisplayTicketList(List<Ticket> tickets)
        {
            Console.WriteLine("\nID\tPriority\tStatus\t\tTitle\t\tAssignee");
            Console.WriteLine("------------------------------------------------------------------");
            
            foreach (var ticket in tickets)
            {
                string assignee = ticket.AssignedTo != null ? ticket.AssignedTo.Name : "Unassigned";
                Console.WriteLine($"{ticket.Id}\t{ticket.Priority}\t\t{ticket.Status}\t\t{ticket.Title.Substring(0, Math.Min(15, ticket.Title.Length))}{(ticket.Title.Length > 15 ? "..." : "")}\t{assignee}");
            }
        }
        
        static void DisplayTicketDetails(Ticket ticket)
        {
            Console.WriteLine($"\n=== Ticket #{ticket.Id} Details ===");
            Console.WriteLine($"Title: {ticket.Title}");
            Console.WriteLine($"Description: {ticket.Description}");
            Console.WriteLine($"Status: {ticket.Status}");
            Console.WriteLine($"Priority: {ticket.Priority}");
            Console.WriteLine($"Category: {ticket.Category}");
            Console.WriteLine($"Reported By: {ticket.ReportedBy}");
            Console.WriteLine($"Reported On: {ticket.CreatedDate}");
            Console.WriteLine($"Assigned To: {(ticket.AssignedTo != null ? ticket.AssignedTo.Name : "Unassigned")}");
            
            if (ticket.Comments.Count > 0)
            {
                Console.WriteLine("\nComments:");
                foreach (var comment in ticket.Comments)
                {
                    Console.WriteLine($"[{comment.Timestamp}] {comment.Author}: {comment.Text}");
                }
            }
            else
            {
                Console.WriteLine("\nNo comments yet.");
            }
        }
        
        static void SeedData(TicketSystem ticketSystem)
        {
            // Add team members
            ticketSystem.AddTeamMember(new TeamMember { Name = "John Doe", Role = "Developer" });
            ticketSystem.AddTeamMember(new TeamMember { Name = "Jane Smith", Role = "QA Engineer" });
            ticketSystem.AddTeamMember(new TeamMember { Name = "Mike Johnson", Role = "DevOps" });
            ticketSystem.AddTeamMember(new TeamMember { Name = "Lisa Chen", Role = "Product Manager" });
            
            // Add some sample tickets
            var ticket1 = new Ticket
            {
                Title = "Login page error",
                Description = "Users are unable to login after the recent update",
                Priority = Priority.High,
                Category = TicketCategory.Bug,
                ReportedBy = "Customer Support"
            };
            ticketSystem.AddTicket(ticket1);
            
            var ticket2 = new Ticket
            {
                Title = "Add export feature",
                Description = "Users need to export their data to CSV",
                Priority = Priority.Medium,
                Category = TicketCategory.Feature,
                ReportedBy = "Product Team"
            };
            ticketSystem.AddTicket(ticket2);
            
            var ticket3 = new Ticket
            {
                Title = "Slow database queries",
                Description = "The system slows down during peak hours",
                Priority = Priority.Critical,
                Category = TicketCategory.Performance,
                ReportedBy = "Operations"
            };
            ticketSystem.AddTicket(ticket3);
            
            // Assign tickets
            ticketSystem.AssignTicket(1, ticketSystem.GetTeamMembers()[0]); // Assign to John
            ticketSystem.AssignTicket(3, ticketSystem.GetTeamMembers()[2]); // Assign to Mike
            
            // Update statuses
            ticketSystem.UpdateTicketStatus(1, TicketStatus.InProgress);
            
            // Add comments
            ticketSystem.AddCommentToTicket(1, "John Doe", "I'm investigating this issue");
            ticketSystem.AddCommentToTicket(1, "Lisa Chen", "This is a high priority for our customers");
        }
        
        #endregion
}