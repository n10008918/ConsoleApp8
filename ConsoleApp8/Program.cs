using System;

namespace ConsoleApp8
{
    internal class Program
    {
        private static MovieGenre movieGenreEnum;
        private static MovieClassification movieClassificationEnum;

        private static string parseStringInput(string prompt)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            return input;
        }

        private static bool isValidMemberData(string firstName, string lastName, string phonenumber, string pin)
        {
            if (firstName == "" || lastName == "" || phonenumber == "" || pin == "")
            {
                Console.WriteLine("All member's details must be provided !");
                return false;
            }

            if (IMember.IsValidContactNumber(phonenumber.ToString()) != true)
            {
                Console.WriteLine("Invalid phone number!");
                Console.WriteLine("Phone number must have has 10 digits, and the first digit is a ‘0’.");
                return false;
            }

            if (IMember.IsValidPin(pin.ToString()) != true)
            {
                Console.WriteLine("Invalid PIN!");
                Console.WriteLine("PIN length must have 4-6 digits.");
                return false;
            }

            return true;
        }

        private static bool isValidMovieData(string movieTitle, string movieGenre, string movieClassification, string movieDuration, string movieCopies)
        {
            if (movieTitle == "" || movieGenre == "" || movieClassification == "" || movieDuration == "" || movieCopies == "")
            {
                Console.WriteLine("All movie details must be provided!");
                return false;
            }

            bool isNumberGenre = int.TryParse(movieGenre, out _);
            bool isNumberClassification = int.TryParse(movieClassification, out _);
            bool isNumberDuration = int.TryParse(movieDuration, out _);
            bool isNumberCopies = int.TryParse(movieCopies, out _);

            if (!isNumberGenre)
            {
                Console.WriteLine("Invalid movie genre!");
                Console.WriteLine("Movie genre selection must be a value.");
                return false;
            }

            if (Convert.ToInt32(movieGenre) < 1 || Convert.ToInt32(movieGenre) > 5)
            {
                Console.WriteLine("Invalid movie genre!");
                Console.WriteLine("Movie genre selection must be from 1-5.");
                return false;
            }

            if (!isNumberClassification)
            {
                Console.WriteLine("Invalid movie classification!");
                Console.WriteLine("Movie classification selection must be a value.");
                return false;
            }

            if (Convert.ToInt32(movieClassification) < 1 || Convert.ToInt32(movieClassification) > 4)
            {
                Console.WriteLine("Invalid movie classification!");
                Console.WriteLine("Movie classification selection must be from 1-4.");
                return false;
            }

            if (!isNumberDuration)
            {
                Console.WriteLine("Invalid movie duration!");
                Console.WriteLine("Movie duration must be an integer.");
                return false;
            }

            if (Convert.ToInt32(movieDuration) < 1 )
            {
                Console.WriteLine("Invalid movie duration!");
                Console.WriteLine("Movie duration must be a positive integer.");
                return false;
            }

            if (!isNumberCopies)
            {
                Console.WriteLine("Invalid movie copies!");
                Console.WriteLine("Movie copies must be an integer.");
                return false;
            }

            if (Convert.ToInt32(movieCopies) < 1)
            {
                Console.WriteLine("Invalid movie copies!");
                Console.WriteLine("Movie copies must be at least 1.");
                return false;
            }

            return true;
        }

        private static void displayMainMenu()
        {
            Console.WriteLine("\n============================================================");
            Console.WriteLine("Welcome to the Community Library Movie DVD Management System");
            Console.WriteLine("============================================================");
            Console.WriteLine("\n====================Main Menu====================\n");
            Console.WriteLine("1. Staff Login");
            Console.WriteLine("2. Member Login");
            Console.WriteLine("0. Exit");
            Console.WriteLine("\nEnter your choice ==> (1/2/0)");
        }

        private static void displayStaffMenu()
        {
            Console.WriteLine("\n====================Staff Menu====================\n");
            Console.WriteLine("1. Add new DVDs of a new movie to the system");
            Console.WriteLine("2. Remove DVDs of a movie from the system");
            Console.WriteLine("3. Register a new member with the system");
            Console.WriteLine("4. Remove a registered member from the system");
            Console.WriteLine("5. Display a member's contact number, given the member's name");
            Console.WriteLine("6. Dsiplay all members who are currently renting a particular movie");
            Console.WriteLine("0. Return to main menu");
            Console.WriteLine("\n\nEnter your choice ==> (1/2/3/4/5/6/0)");
        }

        private static void displayMemberMenu()
        {
            Console.WriteLine("\n====================Member Menu====================\n");
            Console.WriteLine("1. Browse all the movies");
            Console.WriteLine("2. Display all the information about a movie, given the title of the movie");
            Console.WriteLine("3. Borrow a movie DVD");
            Console.WriteLine("4. Return a movie DVD");
            Console.WriteLine("5. List current borrowing movies");
            Console.WriteLine("6. Display the top 3 movies rented by the members");
            Console.WriteLine("0. Return to main menu");
            Console.WriteLine("\n\nEnter your choice ==> (1/2/3/4/5/6/0)");
        }

        private static void processStaffMenu(MovieLibrarySystem movieLibrarySystem)
        {
            Console.Clear();
            Console.WriteLine("\n====================Staff Login ====================\n");
            string staffUsername = parseStringInput("Please enter your username: ");
            string staffPassword = parseStringInput("Please enter your password: ");

            while (true)
            {
                if (staffUsername == "staff" && staffPassword == "today123")
                {
                    Console.WriteLine("Login successful!");
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect login credentials");
                    Console.WriteLine("Enter any key to try again OR Enter 0 to return to main menu");
                    string select = Console.ReadLine();
                    if (select == "0")
                    {
                        return;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n====================Staff Login ====================\n");
                        staffUsername = parseStringInput("Please enter your username: ");
                        staffPassword = parseStringInput("Please enter your password: ");
                    }
                }
            }

            displayStaffMenu();
            string option = Console.ReadLine();

            while (option != "0")
            {
                if (option == "1")
                {
                    Console.WriteLine();
                    string movieTitle = parseStringInput("Please enter the movie's title: ");

                    IMovie searchedMovie = movieLibrarySystem.SearchMovie(movieTitle);

                    if (searchedMovie == null)
                    {
                        Console.WriteLine("Movie Genre: Action = 1, Comedy = 2, History = 3, Drama = 4, Western = 5");
                        string movieGenre = parseStringInput("Please enter the number accordingly for the movie genre: ");
                        Console.WriteLine("Movie Classification: General(G) = 1, Parental Guidance(PG) = 2, Mature(M) = 3, Mature Accompanied(MA15 +) = 4");
                        string movieClassification = parseStringInput("Please enter the number accordingly for the movie classification: ");
                        string movieDuration = parseStringInput("Please enter the movie duration length: ");
                        string movieCopies = parseStringInput("Please enter the total number of DVD's copies: ");

                        while (!isValidMovieData(movieTitle, movieGenre, movieClassification, movieDuration, movieCopies))
                        {
                            Console.WriteLine();
                            movieTitle = parseStringInput("Please enter the movie's title: ");
                            Console.WriteLine("Movie Genre: Action = 1, Comedy = 2, History = 3, Drama = 4, Western = 5");
                            movieGenre = parseStringInput("Please enter the number accordingly for the movie genre: ");
                            Console.WriteLine("Movie Classification: General(G) = 1, Parental Guidance(PG) = 2, Mature(M) = 3, Mature Accompanied(MA15 +) = 4");
                            movieClassification = parseStringInput("Please enter the number accordingly for the movie classification: ");
                            movieDuration = parseStringInput("Please enter the movie duration length: ");
                            movieCopies = parseStringInput("Please enter the total number of DVD's copies: ");
                        }

                        int genre= Convert.ToInt32(movieGenre);
                        int classification = Convert.ToInt32(movieClassification);
                        int duration = Convert.ToInt32(movieDuration);
                        int copies = Convert.ToInt32(movieCopies);

                        if (genre == 1)
                        {
                            movieGenreEnum = MovieGenre.Action;
                        }
                        else if (genre == 2)
                        {
                            movieGenreEnum = MovieGenre.Comedy;
                        }
                        else if (genre == 3)
                        {
                            movieGenreEnum = MovieGenre.History;
                        }
                        else if (genre == 4)
                        {
                            movieGenreEnum = MovieGenre.Drama;
                        }
                        else
                        {
                            movieGenreEnum = MovieGenre.Western;
                        }

                        if (classification == 1)
                        {
                            movieClassificationEnum = MovieClassification.G;
                        }
                        else if (classification == 2)
                        {
                            movieClassificationEnum = MovieClassification.PG;
                        }
                        else if (classification == 3)
                        {
                            movieClassificationEnum = MovieClassification.M;
                        }
                        else
                        {
                            movieClassificationEnum = MovieClassification.M15Plus;
                        }

                        movieLibrarySystem.AddNewMovieCollection(new Movie(movieTitle, movieGenreEnum, movieClassificationEnum, duration, copies));
                    }

                    else
                    {
                        movieLibrarySystem.AddDVD(movieTitle);
                    }
                    
                }
                else if (option == "2")
                {
                    Console.WriteLine();
                    string movieTitle = parseStringInput("Please enter the movie's title: ");
                    movieLibrarySystem.RemoveDVD(movieTitle);

                    
                }
                else if (option == "3")
                {
                    Console.WriteLine();
                    string firstName = parseStringInput("Please enter member's first name: ");
                    string lastName = parseStringInput("Please enter member's last name: ");
                    string phoneNumber = parseStringInput("Please enter member's phone number: ");
                    string pin = parseStringInput("Please enter member's PIN: ");

                    while (!isValidMemberData(firstName, lastName, phoneNumber, pin))
                    {
                        Console.WriteLine();
                        firstName = parseStringInput("Please enter member's first name: ");
                        lastName = parseStringInput("Please enter member's last name: ");
                        phoneNumber = parseStringInput("Please enter member's phone number: ");
                        pin = parseStringInput("Please enter member's PIN: ");
                    }
                    movieLibrarySystem.AddMember(new Member(firstName, lastName, phoneNumber, pin));
                }
                else if (option == "4")
                {
                    Console.WriteLine();
                    string firstName = parseStringInput("Please enter member's first name: ");
                    string lastName = parseStringInput("Please enter member's last name: ");
                    movieLibrarySystem.RemoveMember(new Member(firstName, lastName));
                }
                else if (option == "5")
                {
                    Console.WriteLine();
                    string firstName = parseStringInput("Please enter member's first name: ");
                    string lastName = parseStringInput("Please enter member's last name: ");
                    movieLibrarySystem.DisplayPhoneNumber(new Member(firstName, lastName));
                }
                else if (option == "6")
                {
                    // Display all the borrowers of this movie
                    Console.WriteLine();
                    string movieTitle = parseStringInput("Please enter the movie's title: ");                    
                    movieLibrarySystem.DisplayAllBorrowers(movieTitle);
                }
                else
                {
                    Console.WriteLine("Invalid Choice");                   
                }
                
                Console.WriteLine();
                displayStaffMenu();
                option = Console.ReadLine();
            }
        }

        private static void processMemberMenu(MovieLibrarySystem movieLibrarySystem)
        {
            Console.Clear();
            Console.WriteLine("\n====================Member Login ====================\n");
            string firstName = parseStringInput("Please enter your first name: ");
            string lastName = parseStringInput("Please enter your last name: ");
            string pin = parseStringInput("Please enter your PIN: ");
            IMember member = movieLibrarySystem.SearchMember(new Member(firstName, lastName));

            while (true)
            {
                if (member == null)
                {
                    Console.WriteLine("Invalid member details");
                    Console.WriteLine("Enter any key to try again OR Enter 0 to return to main menu");
                    string select = Console.ReadLine();
                    if (select == "0")
                    {
                        return;
                    }
                }
                else if (member.Pin != pin)
                {
                    Console.WriteLine("Invalid PIN");
                    Console.WriteLine("Enter any key to try again OR Enter 0 to return to main menu");
                    string select = Console.ReadLine();
                    if (select == "0")
                    {
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Login successful!");
                    break;
                }

                Console.Clear();
                Console.WriteLine("\n====================Member Login ====================\n");
                firstName = parseStringInput("Please enter your first name: ");
                lastName = parseStringInput("Please enter your last name: ");
                pin = parseStringInput("Please enter your PIN: ");
                member = movieLibrarySystem.SearchMember(new Member(firstName, lastName));
            }

            displayMemberMenu();
            string option = Console.ReadLine();

            while (option != "0")
            {
                if (option == "1")
                {
                    movieLibrarySystem.DisplayMovies();
                }
                else if (option == "2")
                {
                    Console.WriteLine();
                    string movieTitle = parseStringInput("Enter the title of the movie name to view the information: ");
                    movieLibrarySystem.DisplayInfo(movieTitle);

                }
                else if (option == "3")
                {
                    string movieTitle  = parseStringInput("The name of the movie to borrow: ");
                    movieLibrarySystem.BorrowDvd(member, movieTitle);
                }
                else if (option == "4")
                {
                    string movieTitle = parseStringInput("The name of the movie to return: ");
                    movieLibrarySystem.ReturnDvd(member, movieTitle);

                }
                else if (option == "5")
                {
                    Console.WriteLine();
                    movieLibrarySystem.DisplayMovie(member);
                }
                else if (option == "6")
                {
                    Console.WriteLine();
                    movieLibrarySystem.DisplayTopThree();                  
                }
                else
                {
                    Console.WriteLine("Invalid Choice! Please try again!");
                }
                
                Console.WriteLine();
                displayMemberMenu();
                option = Console.ReadLine();
            }
        }

        static void Main()
        {
            MovieLibrarySystem movieLibrarySystem = new MovieLibrarySystem();

            displayMainMenu();
            string option = Console.ReadLine();
            while (option != "0")
            {
                if (option == "1")
                {
                    processStaffMenu(movieLibrarySystem);
                }
                else if (option == "2")
                {
                    processMemberMenu(movieLibrarySystem);
                }
                else
                {
                    Console.WriteLine("Invalid Choice! Please try again!");
                }
                Console.WriteLine();
                displayMainMenu();
                option = Console.ReadLine();
            }
        }
    }
}
