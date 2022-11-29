
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    public class MovieLibrarySystem
    {
        public static MemberCollection allMembers = new MemberCollection(10);
        public static MovieCollection allMovies = new MovieCollection();

        /// <summary>
        /// Search if a member exists
        /// </summary>
        /// <param name="aMember"></param>
        /// <returns>IMember, all details of the member</returns>
        public IMember SearchMember(IMember aMember)
        {
            IMember searchedMember = allMembers.Find(aMember);
            if (searchedMember == null)
            {                
                return null;
            }
            else
            {
                return searchedMember;
            }
        }

        /// <summary>
        /// Search if a movie exists using the movie title
        /// </summary>
        /// <param name="movieTitle"></param>
        /// <returns>IMovie, all details of the movie</returns>
        public IMovie SearchMovie(string movieTitle)
        {
            IMovie searchedMovie = allMovies.Search(movieTitle);
            if (searchedMovie == null)
            {
                return null;
            }
            else
            {
                return searchedMovie;
            }
        }

        /// <summary>
        /// Add new DVDs of a movie to the system
        ///  If the movie is new (currently no DVD of this movie), then all the information about the movie should be entered
        /// <param name="aMovie"></param>
        public void AddNewMovieCollection(IMovie aMovie)
        {
            // insert new movie collection with all details about the movie
            allMovies.Insert(aMovie);
            Console.WriteLine("New movie collection added to the system!");
        }

        /// <summary>
        /// Add new DVDs of a movie to the system
        /// If the movie is not new (the library has some DVDs of this movie), then only the total quantity needs to be updated
        /// </summary>
        /// <param name="movieTitle"></param>
        public void AddDVD(string movieTitle)
        {
            // insert new DVD into current existing movie collection
            IMovie searchedMovie = allMovies.Search(movieTitle);
            searchedMovie.AvailableCopies++;
            searchedMovie.TotalCopies++;
            Console.WriteLine("1 DVD is added.");
            Console.WriteLine("There is a total of " + searchedMovie.TotalCopies + " DVDs now.");
        }

        /// <summary>
        /// Remove DVDs of a movie from the system. 
        /// If all the DVDs of the movie are removed, the movie should also be removed from the system.
        /// </summary>
        /// <param name="movieTitle"></param>
        public void RemoveDVD(string movieTitle)
        {
            IMovie searchedMovie = allMovies.Search(movieTitle);

            // check if movie exists in the system 
            if (searchedMovie == null)
            {
                Console.WriteLine("Movie does not exist in the system!");
                return;
            }
            else
            {
                // check if it is the last copy of the movie DVD
                if (searchedMovie.TotalCopies > 1)
                {
                    searchedMovie.AvailableCopies--;
                    searchedMovie.TotalCopies--;
                    Console.WriteLine("1 DVD is removed.");
                    Console.WriteLine("There is a total of " + searchedMovie.TotalCopies + " DVDs now.");
                }

                else
                {
                    allMovies.Delete(searchedMovie);
                    Console.WriteLine("Last DVD is removed.");
                    Console.WriteLine("The movie " + searchedMovie.Title + " is removed from the system.");
                }
            }
        }

        /// <summary>
        /// Register a new member with the system
        /// CONDITION: system must check if phone number and password are valid
        /// </summary>
        /// <param name="aMember"></param>
        public void AddMember(IMember aMember)
        {
            IMember searchedMember = allMembers.Find(aMember);

            // check if this member already exists
            if (searchedMember == null)
            {
                allMembers.Add(aMember);
                Console.WriteLine("New member added to the system!");
            }
            else
            {
                Console.WriteLine("Member already exists in the system!");
            }
        }

        /// <summary>
        /// Remove a registered member from the system. 
        /// CONDITION: A registered member cannot be removed if he/she has any movie DVD on loan currently
        /// </summary>
        /// <param name="aMember"></param>
        public void RemoveMember(IMember aMember)
        {
            IMember searchedMember = allMembers.Find(aMember);

            // check if this member exists
            if (searchedMember == null)
            {
                Console.WriteLine("Member does not exist in the system!");
                return;
            }

            // check if this user is currently borrowing any DVD
            IMovie[] list = allMovies.ToArray();
            bool isBorrower = false;
            for (int i = 0; i < list.Length; i++)
            {
                IMovie movie = list[i];
                if (movie.Borrowers.Search(searchedMember))
                {
                    isBorrower = true;
                    break;
                }
            }

            if (isBorrower)
            {
                Console.WriteLine("Member cannot be removed from the system!");
                Console.WriteLine("This member still has movie DVD on loan currently!");
            }
            else
            {
                allMembers.Delete(aMember);
                Console.WriteLine("Member removed from the system!");
            }           
        }

        /// <summary>
        /// Display a member’s contact phone number, given the member’s full name. 
        /// </summary>
        /// <param name="aMember"></param>
        public void DisplayPhoneNumber(IMember aMember)
        {
            IMember searchedMember = allMembers.Find(aMember);

            // check if this member exists
            if (searchedMember == null)
            {
                Console.WriteLine("Member does not exist in the system!");
                return;
            }

            Console.WriteLine(searchedMember.FirstName + " " + searchedMember.LastName + "'s contact number is " + searchedMember.ContactNumber);
        }


        /// <summary>
        /// Display all the members who are currently renting a particular movie
        /// </summary>
        /// <param name="movieTitle"></param>
        public void DisplayAllBorrowers(string movieTitle)
        {
            IMovie movie = allMovies.Search(movieTitle);

            // check if the specific movie exists 
            if (movie == null)
            {
                Console.WriteLine("Movie \"" + movieTitle + "\" does not exist in the system!");
                return;
            }
            // check if the anyone is currently borrowing the movie 
            else if (movie.Borrowers.Number == 0)
            {
                Console.WriteLine("Movie \"" + movieTitle + "\" is not borrowed by anyone!");
            }
            else
            {
                Console.WriteLine("Movie \"" + movieTitle + "\" is borrowed by!");
                Console.WriteLine(movie.Borrowers.ToString());
            }
        }

        /// <summary>
        /// Display information about all movie DVDs in alphabetical order of the movie title, 
        /// including the number of the movie DVDs currently in the community library
        /// </summary>
        public void DisplayMovies()
        {
            IMovie[] movies = allMovies.ToArray();

            // check if there is any movies in the system 
            if (movies.Length == 0)
            {
                Console.WriteLine("No movies in the system!");
                return;
            }
            else 
            {
                for (int i = 0; i < movies.Length; i++)
                {
                    Console.WriteLine("Movie title: " + movies[i].Title.ToString());
                    Console.WriteLine("Available Copies: " + movies[i].AvailableCopies.ToString());
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Display the information about a movie, given the title of the movie
        /// </summary>
        /// <param name="movieTitle"></param>
        public void DisplayInfo(string movieTitle)
        {
            IMovie movie = allMovies.Search(movieTitle);

            // check if the specific movie exists 
            if (movie == null)
            {
                Console.WriteLine("The movie doesn't exist in the system!.");
                return;
            }
            else
            {
                Console.WriteLine(); 
                Console.WriteLine("Movie title: " + movie.Title.ToString());
                Console.WriteLine("Movie Genre: " + movie.Genre.ToString());
                Console.WriteLine("Movie Classification: " + movie.Classification.ToString());
                Console.WriteLine("Movie Duration: " + movie.Duration.ToString());
                Console.WriteLine("Available Copies(DVDs): " + movie.AvailableCopies.ToString());
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Borrow a movie DVD from the community library
        /// </summary>
        /// <param name="aMember"></param>
        /// <param name="movieTitle"></param>
        public void BorrowDvd(IMember aMember, string movieTitle)
        {
            IMember searchMember = allMembers.Find(aMember);

            // check if the specific member exists 
            if (searchMember == null)
            {
                Console.WriteLine("Member does not exist in the system!");
                return;
            }

            // check how many DVDs has been borrowed by the member 
            IMovie[] movielist = allMovies.ToArray();
            List<string> currentBorrowedMovies = new List<string>();
            for (int i = 0; i < movielist.Length; i++)
            {
                IMovie movie = movielist[i];
                if (movie.Borrowers.Search(aMember))
                {
                    currentBorrowedMovies.Add(movie.Title);
                }
            }

            // check if the specific movie exists 
            IMovie searchMovie = allMovies.Search(movieTitle);
            if (searchMovie == null)
            {
                Console.WriteLine("Movie \"" + movieTitle + "\" does not exist in the system!");
            }
            else if (searchMovie.AvailableCopies == 0)
            {
                Console.WriteLine("There is no available copies to be borrowed!");
            }
            else if (currentBorrowedMovies.Count >= 5)
            {
                Console.WriteLine("You can't borrow more than 5 DVDs!");
            }
            else
            {
                // check if member is currently holding the same movie
                bool borrowAccess = searchMovie.AddBorrower(aMember);
                if (borrowAccess == false)
                {
                    Console.WriteLine("This Movie (DVD) cannot be borrowed again!!");
                    Console.WriteLine("You are currently borrowing this Movie (DVD)!!");
                }
                else
                {
                    Console.WriteLine("Successfully borrowed \"" + searchMovie.Title.ToString() + "\" !!");
                }
            }
        }

        /// <summary>
        /// Return a movie DVD to the community library
        /// </summary>
        /// <param name="aMember"></param>
        /// <param name="movieTitle"></param>
        public void ReturnDvd(IMember aMember, string movieTitle)
        {
            IMember searchMember = allMembers.Find(aMember);

            // check if the specific member exists 
            if (searchMember == null)
            {
                Console.WriteLine("Member does not exist in the system!");
                return;
            }

            // check if the specific movie exists 
            IMovie searchMovie = allMovies.Search(movieTitle);
            if (searchMovie == null)
            {
                Console.WriteLine("Movie \"" + movieTitle + "\" does not exist in the system!");
            }
            else
            {
                // check if member borrowed the specific movie
                bool returnAccess = searchMovie.RemoveBorrower(aMember);
                if (returnAccess == false)
                {
                    Console.WriteLine("You did not borrow this Movie (DVD) \"" + movieTitle + "\" !!");
                }
                else
                {
                    Console.WriteLine("Successfully returned \"" + searchMovie.Title.ToString() + "\" !!");
                }
            }
        }

        /// <summary>
        /// List current movies that are currently borrowed by the registered member
        /// </summary>
        /// <param name="member"></param>
        public void DisplayMovie(IMember member)
        {
            IMovie[] list = allMovies.ToArray();
            int count = 0;
            Console.WriteLine("You are currently borrowing: ");

            // Search if movie borrowers list contains the specific member
            for (int i = 0; i < list.Length; i++)
            {
                IMovie movie = list[i];
                if (movie.Borrowers.Search(member))
                {
                    Console.WriteLine(movie.Title);
                    count++;
                }
            }
            if (count == 0)
            {
                Console.WriteLine("None!");
            }
        }

        /// <summary>
        /// Display the top three (3) most frequently borrowed movies by the members in the descending order of their frequency
        /// </summary>
        public void DisplayTopThree()
        {
            IMovie[] list = allMovies.ToArray();

            // sort the list based on borrowed frequency (number of times borrowed)
            for (int i = 0; i < list.Length; i++)
            {
                for (int j = i; j < list.Length; j++)
                {
                    if (list[i].NoBorrowings < list[j].NoBorrowings)
                    {
                        IMovie temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
            }

            // Check if any movie exists in the system
            if (list.Length == 0)
            {
                Console.WriteLine("No movies in the system!");
            }

            // Print the top three movies borrowed
            Console.WriteLine("Top three most frequently borrowed movies:");
            if (list.Length <= 3)
            {
                for (int i = 0; i < list.Length; i++)
                {
                    Console.WriteLine("Title: " + list[i].Title + ", Frequency: " + list[i].NoBorrowings);
                }
            } 
            else 
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine("Title: " + list[i].Title + ", Frequency: " + list[i].NoBorrowings);
                }
            }
        }
    }
}

