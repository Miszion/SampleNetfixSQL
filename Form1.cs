//  Netflix  Database  Application  using  N-Tier  Design.//
//  <<Mission Marcus>
//  U.  of  Illinois,  Chicago
//  CS341,  Spring  2018
//  Project  08
//









using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetflixApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           





        }

        private void button1_Click(object sender, EventArgs e)
        {

            string name = textBox1.Text;
            MovieBox.Items.Clear();
            AllUserBox.Items.Clear();


            BusinessTier.Business bis = new BusinessTier.Business(textBox1.Text);





            var movies = bis.GetAllMovies();

            foreach (var mov in movies)
            {
                MovieBox.Items.Add(mov.MovieName); // fill the user box also.
            }

            amountOfMovies = MovieBox.Items.Count;

            MovieBox.SelectedIndex = 0; // start the index at 0..

            var list = bis.GetAllNamedUsers();

            foreach (var user in list)
            {
                AllUserBox.Items.Add(user.UserName); // fill the user box also.
            }

            AllUserBox.SelectedIndex = 0; // select that to 0th index


        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedText = string.Format("{0}", MovieBox.Text);


            BusinessTier.Business bis = new BusinessTier.Business(textBox1.Text);

           BusinessTier.MovieDetail movD = bis.GetMovieDetail(bis.GetMovie(selectedText).MovieID); // get the movie detail object

            MovieID.Text = string.Format("{0}", (bis.GetMovie(selectedText).MovieID)); // format the movie id 
            Rating.Text = string.Format("{0}", movD.AvgRating); // format the avg rating.

            //start of rating box..

            MovieReview.Items.Clear(); // clear all the items

            string movieToReview = MovieBox.Text;

            BusinessTier.Business bis2 = new BusinessTier.Business(textBox1.Text);

            BusinessTier.Movie mov = bis2.GetMovie(movieToReview); // grab the movie that is to be reviewed.

         
                BusinessTier.MovieDetail movD2 = bis2.GetMovieDetail(mov.MovieID); // select the movie detail

                var list = movD2.Reviews; // make a list of reviews

                MovieReview.Items.Add(movieToReview); // add the name to the top
                MovieReview.Items.Add("");

                if (list.Count == 0)
                {
                    MovieReview.Items.Add("No Reviews Available"); // if no reviews, say it.
                }
                else
                {
                    foreach (var user in list)
                    {
                        string comp = string.Format("{0} : {1}", user.UserID, user.Rating);

                        MovieReview.Items.Add(comp); // add the user id and the ratings.
                    }
                }

            RatingBox.Items.Clear();

            RatingBox.Items.Add(selectedText); // in rating box, we want to set up the ratings too.
            RatingBox.Items.Add("");

            int how5 = 0;
            int how4 = 0;
            int how3 = 0;
            int how2 = 0;
            int how1 = 0;

            foreach(var user in list) // pretty much count how much of each rating exist using 5 variables.
            {
                if (user.Rating == 5)
                {
                    how5++;
                }
                else if (user.Rating == 4)
                {
                    how4++;
                }
                else if (user.Rating == 3)
                {
                    how3++;
                }
                else if (user.Rating == 2)
                {
                    how2++;
                }
                else
                {
                    how1++;
                }
            }

            RatingBox.Items.Add(string.Format("5: {0}", how5)); // format them all and then just spit out the count.
            RatingBox.Items.Add(string.Format("4: {0}", how4));
            RatingBox.Items.Add(string.Format("3: {0}", how3));
            RatingBox.Items.Add(string.Format("2: {0}", how2));
            RatingBox.Items.Add(string.Format("1: {0}", how1));
            RatingBox.Items.Add("");
            RatingBox.Items.Add("");
            RatingBox.Items.Add(string.Format("Total: {0}", list.Count));
        }

        private void AllUserBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedText = AllUserBox.Text;

            BusinessTier.Business bis = new BusinessTier.Business(textBox1.Text);
            BusinessTier.User use = bis.GetNamedUser(selectedText);

            string occu = use.Occupation; // find the occupation
            string userID = string.Format("{0}", use.UserID); // find the user id

            if (occu == "") // if no occupation..
            {
               OccupationBox.Text = "N/A";
            }
            else
            {
                OccupationBox.Text = occu;
            }

            UserIDBox.Text = userID; // get the userid.


            UserReviews.Items.Clear(); // clear all the reviews

            BusinessTier.Business bis2 = new BusinessTier.Business(textBox1.Text);
            BusinessTier.User use2 = bis2.GetNamedUser(selectedText);
            BusinessTier.UserDetail use3 = bis2.GetUserDetail(use2.UserID);

            UserReviews.Items.Add(selectedText);
            UserReviews.Items.Add("");

            var list = use3.Reviews;

            if (list.Count == 0) // if the length of the list is 0.. then no reviews
            {
                UserReviews.Items.Add("No Reviews Given");
            }
            else
            {
                foreach(var used in list)
                {
                    string added = string.Format("{0} -> {1}", bis2.GetMovie(used.MovieID).MovieName, used.Rating); // put all movie names and ratings.
                    UserReviews.Items.Add(added);
                }
            }



        }

        private void ReviewButton_Click(object sender, EventArgs e)
        {

            string movieToReview = ReviewBox.Text;
            BusinessTier.Business bis = new BusinessTier.Business(textBox1.Text);

            BusinessTier.Movie mov = bis.GetMovie(movieToReview);

            if (mov == null) // if the movie isnt there..
            {
                MessageBox.Show("Movie Does Not Exist");
            }
            else
            {
                MovieReview.Items.Clear();
                BusinessTier.MovieDetail movD = bis.GetMovieDetail(mov.MovieID);

                var list = movD.Reviews; // make the list of reviews

                MovieReview.Items.Add(movieToReview);
                MovieReview.Items.Add("");

                if (list.Count == 0)
                {
                    MovieReview.Items.Add("No Reviews Available"); // if no reviews...
                }
                else
                {

                    foreach (var user in list)
                    {
                        string comp = string.Format("{0} : {1}", user.UserID, user.Rating);

                        MovieReview.Items.Add(comp); // add the user id and user ratings.
                    }
                }
            }
        }

        private void FindUser_Click(object sender, EventArgs e)
        {

            string selectedText = UserRateBox.Text;

            BusinessTier.Business bis2 = new BusinessTier.Business(textBox1.Text);
            BusinessTier.User use2 = bis2.GetNamedUser(selectedText);


            if (use2 == null) // if we are looking for a user that doesnt exist.
            {
                MessageBox.Show("User Does Not Exist");
            }
            else
            {
                UserReviews.Items.Clear(); // clear the reviews

                BusinessTier.UserDetail use3 = bis2.GetUserDetail(use2.UserID); // get all user info


                UserReviews.Items.Add(selectedText);
                UserReviews.Items.Add("");

                var list = use3.Reviews;

                if (list.Count == 0)
                {
                    UserReviews.Items.Add("No Reviews Given"); // if no reviews given..
                }
                else
                {
                    foreach (var used in list)
                    {
                        string added = string.Format("{0} -> {1}", bis2.GetMovie(used.MovieID).MovieName, used.Rating); // put the movie name and rating side by side
                        UserReviews.Items.Add(added);
                    }
                }
            }
        }

        private void Calculate_Click(object sender, EventArgs e)
        {
            string userName = InsertU.Text; // get the username moviename and the rating
            string movieName = MovieU.Text;
            int rating = Convert.ToInt32(RatingU.Text);

            BusinessTier.Business bis = new BusinessTier.Business(textBox1.Text);
            BusinessTier.User use = bis.GetNamedUser(userName);
            BusinessTier.Movie mov = bis.GetMovie(movieName);

            if (use == null) // if user doesnt exist
            {
                MessageBox.Show("Review Add Failed: User Does Not Exist");

            }
            else if (mov == null) // if movie doesnt exist
            {
                MessageBox.Show("Review Add Failed: Movie Does Not Exist");
            }
            else if (rating <= 0 || rating > 5) // if the rating is out of bounds
            {
                MessageBox.Show("Review Add Failed: Rating Must Be Less than 5 and More than 0");
            }
            else
            {

                int userID = use.UserID;
                int movieID = mov.MovieID;



                BusinessTier.Review rev = bis.AddReview(movieID, userID, rating); // just add the review.

         
            }

        }

        private void Check_Click(object sender, EventArgs e)
        {
            string textInsert = RatingMovie.Text; // get movie..


   

            BusinessTier.Business bis = new BusinessTier.Business(textBox1.Text);
            BusinessTier.Movie mov = bis.GetMovie(textInsert);

            if (mov == null) // if the movie doesnt exist..
            {
                MessageBox.Show("Movie Does Not Exist");
            }
            else
            {

                BusinessTier.MovieDetail movD2 = bis.GetMovieDetail(mov.MovieID);

                var list = movD2.Reviews;

                RatingBox.Items.Clear();

                RatingBox.Items.Add(textInsert);
                RatingBox.Items.Add("");

                int how5 = 0;
                int how4 = 0;
                int how3 = 0;
                int how2 = 0;
                int how1 = 0;

                foreach (var user in list) // count how much of each rating exists.
                {
                    if (user.Rating == 5)
                    {
                        how5++;
                    }
                    else if (user.Rating == 4)
                    {
                        how4++;
                    }
                    else if (user.Rating == 3)
                    {
                        how3++;
                    }
                    else if (user.Rating == 2)
                    {
                        how2++;
                    }
                    else
                    {
                        how1++;
                    }
                }

                RatingBox.Items.Add(string.Format("5: {0}", how5)); // add them all to the box and then display the count of the list.
                RatingBox.Items.Add(string.Format("4: {0}", how4));
                RatingBox.Items.Add(string.Format("3: {0}", how3));
                RatingBox.Items.Add(string.Format("2: {0}", how2));
                RatingBox.Items.Add(string.Format("1: {0}", how1));
                RatingBox.Items.Add("");
                RatingBox.Items.Add("");
                RatingBox.Items.Add(string.Format("Total: {0}", list.Count));




            }
           
        }

        private void SearchRatings_Click(object sender, EventArgs e)
        {
            string numberof = NumberTop.Text;

            int realNumber = Convert.ToInt32(numberof); // convert to number

            BusinessTier.Business biz = new BusinessTier.Business(textBox1.Text);
            



            if (realNumber < 0 || realNumber > amountOfMovies) // if number is invalid.
            {
                MessageBox.Show("Please Enter Valid Number");
            }
            else
            {

                BusinessTier.Business bis = new BusinessTier.Business(textBox1.Text);
                var list = bis.GetTopMoviesByAvgRating(realNumber);

                TopByRating.Items.Clear(); // clear the rating list..

                foreach (var each in list)
                {
                    string added = string.Format("{0}:{1}", each.MovieName, bis.GetMovieDetail(each.MovieID).AvgRating); // add the moviename and the avg rating of each.
                    TopByRating.Items.Add(added);
                }

            }

        }
    }
}
