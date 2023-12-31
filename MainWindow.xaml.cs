﻿using System;
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

namespace HeadFirst
{
  using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      DispatcherTimer timer = new DispatcherTimer();
      int seconds;
      int matchesFound;
        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;

            SetUpGame();
        }
    private void Timer_Tick(object sender, EventArgs e)
    {
      seconds++;
      timeTextBlock.Text = (seconds/10F).ToString("0.0s");
      if(matchesFound==8)
      {
        timer.Stop();
        timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
      }
    }
    private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
    {
      if(matchesFound==8)
      {
        SetUpGame();
      }
    }

    TextBlock lastTextBlockClicked;
    bool findingMatch = false;

    private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
    {
      TextBlock textBlock = sender as TextBlock;
      if(findingMatch == false)
      {
        textBlock.Visibility = Visibility.Hidden;
        lastTextBlockClicked = textBlock;
        findingMatch = true;
      }
      else if(textBlock.Text == lastTextBlockClicked.Text)
      {
        matchesFound++;
        textBlock.Visibility = Visibility.Hidden;
        findingMatch = false;
      }
      else
      {
        lastTextBlockClicked.Visibility = Visibility.Visible;
        findingMatch = false;
      }
    }

    private void SetUpGame()
    {
      List<string> emojiList = new List<string>()
      {
        "😍", "😍","😄","😄","😅","😅","😈","😈","😎","😎","😝","😝","😠","😠","😤","😤" 
      };
      Random random = new Random();
      foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
      {
        if(textBlock.Name != "timeTextBlock")
        { 
          textBlock.Visibility = Visibility.Visible;
          int index = random.Next(emojiList.Count);
          string nextEmoji = emojiList[index];
          textBlock.Text = nextEmoji;
          emojiList.RemoveAt(index);
        }
        timer.Start();
        seconds = 0;
        matchesFound = 0;
      }
    }
  }
}
