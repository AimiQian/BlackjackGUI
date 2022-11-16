using BlackJackProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

//Assignment 1
//Student: Aimi Qian

namespace BlackjackGUI
{
    public partial class MainWindow : Window


    {
        Blackjack game = new Blackjack();
        //Set initial chip = 100
        int chip = 100;
        int chosenchip = 0;
        MediaPlayer HitMedia = new MediaPlayer();
        //Set if one has A initially or in later cards
        bool hasADealerInitial = false;
        bool hasAPlayerInitial = false;
        bool hasAPlayerNew = false;

        //initialize the original screen
        public MainWindow()
        {
            InitializeComponent();
            DisplayImage();
            HitBtn.Visibility = Visibility.Collapsed;
            StandBtn.Visibility = Visibility.Collapsed;
            tenBtn.IsEnabled = true;
            twentyBtn.IsEnabled = true;
            fiftyBtn.IsEnabled = true;
            PlayerSumText.Text = "Player's Sum: ?";
            DealerSumText.Text = "Dealer's Sum: ?";
            playsound();
        }

        //start the game, deal cards to player and dealer, enable the buttons
        private void gameStart()
        {
            PlaceYourBet.Visibility = Visibility.Collapsed;
            HitBtn.Visibility = Visibility.Visible;
            StandBtn.Visibility = Visibility.Visible;
            game.Start();
            CardInitialized();


            Card playerCard1 = game.PlayersCards[0];
            Card playerCard2 = game.PlayersCards[1];

            Card dealerCard1 = game.DealerCards[0];
            Card dealerCard2 = game.DealerCards[1];

            checkInitialBlackJack(playerCard1, playerCard2, "Player");
            checkInitialBlackJack(dealerCard1, dealerCard2, "Dealer");
        }

        //check if initial cards contian A and if the sum is 21
        private void checkInitialBlackJack(Card card1, Card card2, String name)
        {
            if (card1.Rank == 1)
            {
                if (11 + game.GetBlackjackValue(card2) == 21)
                {
                    
                    /*MessageBox.Show(name + " BlackJack!");*/
                    ButtonDisappear();
                    chip += chosenchip * 2;
                    if(name == "Player") { PlayerChipSum.Text = "Total Chips: " + chip; PlayerSumText.Text = "Player's Sum: 21(A as 11）"; }
                    if(name == "Dealer") { DealerSumText.Text = "Dealer's Sum: 21 (A as 11）"; }
                    MessageText.Text = name + " BlackJacked! " + name + " Wins!";
                    if (name == "Dealer")
                    {
                        Refresh();
                    }
                }
                else
                {
                    if (name == "Player")
                    {
                        PlayerSumText.Text = "Player's Sum: " + game.GetPlayerSum();
                    }
                }
                if (name == "Player")
                {
                    hasAPlayerInitial = true;
                }
                if (name == "Dealer")
                {
                    hasADealerInitial = true;
                }
            }
            else if (card2.Rank == 1)
            {
                if (11 + game.GetBlackjackValue(card1) == 21)
                {
                    /*MessageBox.Show(name + " BlackJack!");*/
                    ButtonDisappear();
                    chip += chosenchip * 2;
                    if (name == "Player") { PlayerChipSum.Text = "Total Chips: " + chip; PlayerSumText.Text = "Player's Sum: 21(A as 11）"; }
                    if (name == "Dealer") { DealerSumText.Text = "Dealer's Sum: 21 (A as 11）"; }
                    MessageText.Text = name + " BlackJacked! " + name + " Wins!";
                    if (name == "Dealer")
                    {
                        Refresh();
                    }
                }
                else 
                {
                    if (name == "Player")
                    {
                        PlayerSumText.Text = "Player's Sum: " + game.GetPlayerSum();
                    }
                }
                if (name == "Player")
                {
                    hasAPlayerInitial = true;
                    
                }
                if (name == "Dealer")
                {
                    hasADealerInitial = true;
                }
            }
            else
            {
                if (name == "Player")
                {
                    PlayerSumText.Text = "Player's Sum: " + game.GetPlayerSum();
                }
            }
        }


        //Add ten bet 
        private void ten_Click(object sender, RoutedEventArgs e)
        {
            twentyBtn.Visibility = Visibility.Collapsed;
            fiftyBtn.Visibility = Visibility.Collapsed;
            tenBtn.IsEnabled = false;
            gameStart();
            chip -= 10;
            chosenchip = 10;
            PlayerChipSum.Text = "Total Chips: " + chip;

        }

        //Add twenty bet 
        private void twenty_Click(object sender, RoutedEventArgs e)
        {
            tenBtn.Visibility = Visibility.Collapsed;
            fiftyBtn.Visibility = Visibility.Collapsed;
            twentyBtn.IsEnabled = false;
            gameStart();
            chip -= 20;
            chosenchip = 20;
            PlayerChipSum.Text = "Total Chips: " + chip;

        }

        //Add fifty bet 
        private void fifty_Click(object sender, RoutedEventArgs e)
        {

            tenBtn.Visibility = Visibility.Collapsed;
            twentyBtn.Visibility = Visibility.Collapsed;
            fiftyBtn.IsEnabled = false;
            gameStart();
            chip -= 50;
            chosenchip = 50;
            PlayerChipSum.Text = "Total Chips: " + chip;

        }

        //Click Hit to add card
        //Decides whether to count A as 1 or 11
        //Anounce the winner and update chips
        private void Hit_Click(object sender, RoutedEventArgs e)
        {
            int playerSumBefore = game.GetPlayerSum();
            int result = game.Hit();
            int playerCardSize = game.PlayersCards.Count;
            DisplayCard(game.PlayersCards[playerCardSize - 1], PlayerPanel);
            Card newCard = game.PlayersCards[playerCardSize - 1];
            PlayerSumText.Text = "Player's Sum: " + game.GetPlayerSum();
            if (hasAPlayerInitial)
            {
                if(game.GetPlayerSum() + 10 == 21)
                {
                    /*                    MessageBox.Show("Player Wins!");*/
                    Stand_Click(sender, e);
                    ButtonDisappear();
                    chip += chosenchip * 2;
                    PlayerChipSum.Text = "Total Chips: " + chip;
                    MessageText.Text = "Player BlackJacked! Player Win!";
                    PlayerSumText.Text = "Player's Sum: 21 (A as 11)";
                    return;
                }
                if (10 + game.GetPlayerSum() < 21)
                {
                    PlayerSumText.Text = "Player's Sum: " + game.GetPlayerSum() + "or" + (game.GetPlayerSum() + 10);
                }
                if (result < 0)
                {
                    Refresh();
                    DealerSumText.Text = "Dealer's Sum: " + game.GetDealerSum();
/*                    MessageBox.Show("Player Burst");*/
                    ButtonDisappear();
/*                    MessageBox.Show("Dealer Wins!");*/
                    MessageText.Text = "Player Burst!Dealer Wins! ";
                }
                else if (result == 1)
                {
                    /*                    MessageBox.Show("Player Wins!");*/
                    Stand_Click(sender, e);
                    ButtonDisappear();
                    chip += chosenchip * 2;
                    PlayerChipSum.Text = "Total Chips: " + chip;
                    MessageText.Text = "Player BlackJacked! Player Win!";
                }
            }
            else
            {
                if (newCard.Rank == 1)
                {
                    hasAPlayerNew = true;
                    if (11 + playerSumBefore == 21)
                    {
                        /*                       MessageBox.Show("Player Wins!");*/
                        Stand_Click(sender, e);
                        MessageText.Text = "Player BlackJacked! Player Win!";
                        ButtonDisappear();
                        chip += chosenchip * 2;
                        PlayerChipSum.Text = "Total Chips: " + chip;
                        PlayerSumText.Text = "Player's Sum: 21 (A as 11）";
                        return;
                    }
                    if (11 + playerSumBefore < 21)
                    {
                        PlayerSumText.Text = "Player's Sum: " + game.GetPlayerSum() + "or" + (game.GetPlayerSum() + 10);
                    }
                    if (result < 0)
                    {
                        Refresh();
                        DealerSumText.Text = "Dealer's Sum: " + game.GetDealerSum();
/*                        MessageBox.Show("Player Burst");*/
                        ButtonDisappear();
/*                        MessageBox.Show("Dealer Wins!");*/
                        MessageText.Text = "Player Burst!Dealer Wins! ";
                    }
                }
                else
                {
                    if (result < 0)
                    {
                        Refresh();
                        DealerSumText.Text = "Dealer's Sum: " + game.GetDealerSum();
/*                        MessageBox.Show("Player Burst");*/
                        ButtonDisappear();
/*                        MessageBox.Show("Dealer Wins!");*/
                        MessageText.Text = "Player Burst!Dealer Wins! ";
                    }
                    else if (result == 1)
                    {
                        /*                        MessageBox.Show("Player Wins!");*/
                        Stand_Click(sender, e);
                        ButtonDisappear();
                        chip += chosenchip * 2;
                        PlayerChipSum.Text = "Total Chips: " + chip;
                        MessageText.Text = "Player BlackJacked! Player Win!";
                    }
                    if (hasAPlayerNew)
                    {
                        if (game.GetPlayerSum() + 10 == 21)
                        {
                            /*                            MessageBox.Show("Player Wins!");*/
                            Stand_Click(sender, e);
                            ButtonDisappear();
                            chip += chosenchip * 2;
                            PlayerChipSum.Text = "Total Chips: " + chip;
                            MessageText.Text = "Player BlackJacked! Player Win!";
                            PlayerSumText.Text = "Player's Sum: 21 (A as 11)";
                            return;
                        }
                        if (10 + game.GetPlayerSum() < 21)
                        {
                            PlayerSumText.Text = "Player's Sum: " + game.GetPlayerSum() + "or" + (game.GetPlayerSum() + 10);
                        }
                        if (result < 0)
                        {
                            Refresh();
                            DealerSumText.Text = "Dealer's Sum: " + game.GetDealerSum();
/*                            MessageBox.Show("Player Burst");*/
                            ButtonDisappear();
/*                            MessageBox.Show("Dealer Wins!");*/
                            MessageText.Text = "Player Burst!Dealer Wins! ";
                        }
                    }
                }
            }
        }


        //Click stand then the dealer continously add cards until the sum is > 16
        //Decides whether to count A as 1 or 11
        //Anounce the winner and update chips
        private void Stand_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            Refresh();
            bool hasA = false;
            while (result == 0)
            {
                Console.WriteLine(hasADealerInitial);
                if (hasADealerInitial)
                {
                    if (game.GetDealerSum() + 10 > 16)
                    {
                        break;
                    }
                }
                int dealerSumBefore = game.GetDealerSum();
                if (dealerSumBefore > 16)
                {
                    break;
                }
                result = game.DealerHit();
                int dealerCardSize = game.DealerCards.Count;
                DisplayCard(game.DealerCards[dealerCardSize - 1], DealerPanel);
                Card newCard = game.DealerCards[dealerCardSize - 1];
                if (hasADealerInitial)
                {
                    if (game.GetDealerSum() + 10 == 21)
                    {
                        /*MessageBox.Show("Delaer Wins!");*/
                        MessageText.Text = "Dealer BlackJacked! Dealer  Win!";
                        ButtonDisappear();
                        DealerSumText.Text = "Dealer's Sum: 21 (A as 11）";
                        return;
                    }
                    if (game.GetDealerSum() + 10 > 16)
                    {
                        break;
                    }
                }
                if (newCard.Rank == 1)
                {
                    hasA = true;
                    if (11 + dealerSumBefore == 21)
                    {
                        /*MessageBox.Show("Delaer Wins!");*/
                        MessageText.Text = "Dealer BlackJacked! Dealer  Win!";
                        ButtonDisappear();
                        DealerSumText.Text = "Dealer's Sum: 21 (A as 11）";
                        return;
                    }
                    if (game.GetDealerSum() + 10 > 16)
                    {
                        break;
                    }
   
                }

            }
            int dealerSum = game.GetDealerSum();
            if (hasA ||hasADealerInitial)
            {
                if(dealerSum + 10 < 21)
                {
                    dealerSum += 10;
                }
            }

            DealerSumText.Text = "Dealer's Sum: " + dealerSum;

            if (dealerSum > 21)
            {
                /*MessageBox.Show("Dealer Burst");*/
                HitBtn.Visibility = Visibility.Collapsed;
                StandBtn.Visibility = Visibility.Collapsed;
                /*MessageBox.Show("Player Wins!");*/
                chip += chosenchip * 2;
                PlayerChipSum.Text = "Total Chips: " + chip;
                MessageText.Text = "Dealer Burst!Player Wins! ";
            }
            else
            {
                int PlayerSum = game.GetPlayerSum();
                if (hasAPlayerNew ||hasAPlayerInitial)
                {
                    if(PlayerSum + 10 < 21)
                    {
                        PlayerSum = PlayerSum + 10;
                    }
                }
                if (dealerSum < PlayerSum)
                {
                    /*MessageBox.Show("Player Wins!");*/
                    MessageText.Text = "Player Wins! ";
                    chip += chosenchip * 2;
                    PlayerChipSum.Text = "Total Chips: " + chip;
                }
                else if (dealerSum == PlayerSum)
                {
                    /*MessageBox.Show("Push!");*/
                    chip += chosenchip;
                    PlayerChipSum.Text = "Total Chips: " + chip;
                    MessageText.Text = "Push";
                }
                else if(dealerSum == 21)
                {
                    /*MessageBox.Show("Dealer Blackjacked! Dealer Wins!");*/
                    MessageText.Text = "Dealer Wins! ";
                }
                else
                {
                    /*MessageBox.Show("Dealer Wins!");*/
                    MessageText.Text = "Dealer Wins! ";
                }
            }
            ButtonDisappear();
        }

        //initialize the original cards of dealer and player
        private void CardInitialized()
        {
            DealerPanel.Children.Clear();
            PlayerPanel.Children.Clear();

            DisplayCard(game.DealerCards[0], DealerPanel);
            DisplayBackCard(DealerPanel);

            foreach (Card c in game.PlayersCards)
            {
                //put this on the screen
                DisplayCard(c, PlayerPanel);
            }
        }

        //refresh the screen to display all the cards
        private void Refresh()
        {
            DealerPanel.Children.Clear();
            PlayerPanel.Children.Clear();

            foreach (Card c in game.DealerCards)
            {
                //put this image on the screen
                DisplayCard(c, DealerPanel);

            }
            foreach (Card c in game.PlayersCards)
            {
                //put this on the screen
                DisplayCard(c, PlayerPanel);
            }
        }

        //The method to display cards by importing card image names
        private void DisplayCard(Card c, WrapPanel panel)
        {
            // Comment out the following string manipution
            //String basePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //string path = basePath.Replace("\\bin\\Debug\\net6.0-windows\\WpfDemo.dll", "") + "/JPEG/" + c.GetImgFilename();

            string path = AppDomain.CurrentDomain.BaseDirectory + @"../../../JPEG/" + c.GetImgFilename();

            Uri uri = new Uri(path);
            BitmapImage bitmapImg = new BitmapImage(uri);
            Image img = new Image();
            img.Source = bitmapImg;
            img.Width = 80;

            panel.Children.Add(img);

        }

        //The method to display the back of one dealer's card
        private void DisplayBackCard(WrapPanel panel)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"../../../JPEG/Red_back.jpg";
            Uri uri = new Uri(path);
            BitmapImage bitmapImg = new BitmapImage(uri);
            Image img = new Image();
            img.Source = bitmapImg;
            img.Width = 80;

            panel.Children.Add(img);
        }

        //The method to hide hit and stand buttons
        private void ButtonDisappear()
        {
            HitBtn.Visibility = Visibility.Collapsed;
            StandBtn.Visibility = Visibility.Collapsed;
        }

        //Renew the chip amount and start rhe game
        private void ReStart_Click(object sender, RoutedEventArgs e)
        {
            Restart();
            chip = 100;
            PlayerChipSum.Text = "Total Chips: " + chip;
        }

        //Keep the chip amount and start rhe game
        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            Restart();
            if(chip <= 0)
            {
                tenBtn.IsEnabled = false;
                twentyBtn.IsEnabled = false;
                fiftyBtn.IsEnabled = false;
                MessageBox.Show("You cannot place bet! No Chip");
                MessageText.Text = "You cannot place bet! No Chip";
            }
        }

        //restart the game
        private void Restart()
        {
            DealerPanel.Children.Clear();
            PlayerPanel.Children.Clear();
            game.DealerCards.Clear();
            game.PlayersCards.Clear();
            ButtonDisappear();
            PlayerPanel.Children.Add(PlaceYourBet);
            tenBtn.Visibility = Visibility.Visible;
            twentyBtn.Visibility = Visibility.Visible;
            fiftyBtn.Visibility = Visibility.Visible;
            PlaceYourBet.Visibility = Visibility.Visible;
            tenBtn.IsEnabled = true;
            twentyBtn.IsEnabled = true;
            fiftyBtn.IsEnabled = true;
            hasADealerInitial = false;
            hasAPlayerInitial = false;
            hasAPlayerNew = false;
            MessageText.Text = "";
            PlayerSumText.Text = "Player's Sum: ?";
            DealerSumText.Text = "Dealer's Sum: ?";
        }

        //Display the initial screen images
        private void DisplayImage()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"../../../JPEG/blackjackname.png";
            Uri uri = new Uri(path);
            BitmapImage bitmapImg = new BitmapImage(uri);
            Image img = new Image();
            img.Source = bitmapImg;
            img.Width = 150;
            Picture1Panel.Children.Add(img);
            string path2 = AppDomain.CurrentDomain.BaseDirectory + @"../../../JPEG/casino.png";
            Uri uri2 = new Uri(path2);
            BitmapImage bitmapImg2 = new BitmapImage(uri2);
            Image img2 = new Image();
            img2.Source = bitmapImg2;
            img2.Width = 150;
            Picture2Panel.Children.Add(img2);
        }
        
        //play the background sound
        private void playsound()
        {
            MediaPlayer playMedia = new MediaPlayer();
            string path = AppDomain.CurrentDomain.BaseDirectory + @"../../../SOUND/background.mp3";
            Uri uri = new Uri(path);
            playMedia.Open(uri);
            playMedia.Play();

        }

    }

}
