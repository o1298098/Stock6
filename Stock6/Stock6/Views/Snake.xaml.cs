using Stock6.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Stock6.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Snake : ContentPage
	{
        public class snakemodel
        {
            public int x { get; set; }
            public int y { get; set; }
        }
        Grid grid ;
        Frame frame;
        List<snakemodel> snake;
        snakemodel applemodel;
        BoxView apple;
        Timer timer;        
        string direction;
        public Snake ()
		{
			InitializeComponent ();
            direction = "right";
            snake = new List<snakemodel> {
                    new snakemodel{x=5,y=6 },
                    new snakemodel{x=6,y=6 },
                    new snakemodel{x=7,y=6 },
                    new snakemodel{x=8,y=6 },
                    new snakemodel{x=9,y=6 },
                };
            Random random = new Random();
            applemodel = new snakemodel {x=random.Next(0, 45),y= random.Next(0,40) };
            grid = new Grid { RowSpacing = 1, ColumnSpacing = 1, HorizontalOptions = LayoutOptions.Center };
            frame = new Frame { BorderColor = Color.Black, BackgroundColor = Color.FromRgba(255, 255, 255, 0) };
            apple = new BoxView { BackgroundColor = Color.Red, };
            this.Appearing+=async delegate
            {
                stack.Children.Clear();
                await snakestart();
            };
            
        }

        private async Task snakestart()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 45; i++)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(7) });
                }
                for (int i = 0; i < 40; i++)
                {
                    grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(7) });
                }
            });
            grid.Children.Add(frame);
            Grid.SetColumnSpan(frame, grid.ColumnDefinitions.Count);
            Grid.SetRowSpan(frame, grid.RowDefinitions.Count);
            grid.Children.Add(apple,applemodel.x,applemodel.y);
            timer = new Timer();
            timer.Interval = 400;
            foreach (var q in snake)
            {
                BoxView box = new BoxView { BackgroundColor = Color.FromHex("BEBEBE"), };
                grid.Children.Add(box, q.x, q.y);
            }
            Image right = new Image { WidthRequest=50,HeightRequest=50, BackgroundColor = Color.FromRgba(255, 255, 255, 0), Source = "righta.png",Aspect=Aspect.Fill};
            Image left = new Image { WidthRequest = 50, HeightRequest = 50, BackgroundColor = Color.FromRgba(255, 255, 255, 0), Source = "lefta.png", Aspect = Aspect.Fill };
            Image up = new Image { WidthRequest = 50, HeightRequest = 50, BackgroundColor = Color.FromRgba(255, 255, 255, 0), Source = "upa.png", Aspect = Aspect.Fill };
            Image down = new Image { WidthRequest = 50, HeightRequest = 50, BackgroundColor = Color.FromRgba(255, 255, 255, 0), Source = "downa.png", Aspect = Aspect.Fill };
            TapGestureRecognizer rtapGesture = new TapGestureRecognizer();
            TapGestureRecognizer ltapGesture = new TapGestureRecognizer();
            TapGestureRecognizer utapGesture = new TapGestureRecognizer();
            TapGestureRecognizer dtapGesture = new TapGestureRecognizer();
            rtapGesture.Tapped += delegate
            {
                if (direction != "left")
                    direction = "right";
            };
            ltapGesture.Tapped += delegate
            {
                if (direction != "right")
                    direction = "left";
            };
            utapGesture.Tapped += delegate
            {
                if (direction != "down")
                    direction = "up";
            };
            dtapGesture.Tapped += delegate
            {
                if (direction != "up")
                    direction = "down";
            };
            right.GestureRecognizers.Add(rtapGesture);
            left.GestureRecognizers.Add(ltapGesture);
            up.GestureRecognizers.Add(utapGesture);
            down.GestureRecognizers.Add(dtapGesture);
            Grid gridpad = new Grid
            {
                Margin=new Thickness(20,5,0,0),
                RowDefinitions =new RowDefinitionCollection {
                    new RowDefinition{Height=new GridLength(50) },
                    new RowDefinition{Height=new GridLength(50) },
                    new RowDefinition{Height=new GridLength(50) },
                },
                ColumnDefinitions =new ColumnDefinitionCollection {
                    new ColumnDefinition{Width=new GridLength(50) },
                    new ColumnDefinition{Width=new GridLength(50) },
                    new ColumnDefinition{Width=new GridLength(50) },
                }
            };
            gridpad.Children.Add(right,2,1);
            gridpad.Children.Add(left,0,1);
            gridpad.Children.Add(up,1,0);
            gridpad.Children.Add(down,1,2);            
            stack.Children.Add(grid);
            stack.Children.Add(gridpad);
            timer.Elapsed += delegate
            {
                Device.BeginInvokeOnMainThread(() =>
                {

                    snakemodel nodemodel = new snakemodel();
                    nodemodel.x=snake[snake.Count - 1].x;
                    nodemodel.y = snake[snake.Count - 1].y;
                    for (int i = 0; i < snake.Count; i++)
                    {
                        grid.Children.RemoveAt(grid.Children.Count - 1);
                    }
                    if (snake[snake.Count - 1].x == applemodel.x && snake[snake.Count - 1].y == applemodel.y)
                    {
                        applemodel.x = new Random().Next(0, grid.ColumnDefinitions.Count);
                        applemodel.y = new Random().Next(0, grid.RowDefinitions.Count);
                        grid.Children.Add(apple, applemodel.x, applemodel.y);
                        snake.Add(nodemodel);
                        timer.Interval = (timer.Interval - 10)<50?50: (timer.Interval - 10);
                    }
                    for (int i = 0; i < snake.Count; i++)
                    {
                        if (i == snake.Count - 1 && i != 0)
                        {
                            if (direction == "right")
                            {
                                snake[i].x = snake[i].x + 1;
                            }
                            else if (direction == "left")
                            {
                                snake[i].x = snake[i].x - 1;
                            }
                            else if (direction == "up")
                            {
                                snake[i].y = snake[i].y - 1;
                            }
                            else if (direction == "down")
                            {
                                snake[i].y = snake[i].y + 1;
                            }
                        }
                        else
                        {
                            snake[i].x = snake[i + 1].x;
                            snake[i].y = snake[i + 1].y;
                        }
                        if (snake[snake.Count - 1].y == grid.RowDefinitions.Count || snake[snake.Count - 1].x == grid.ColumnDefinitions.Count || snake[snake.Count - 1].x < 0 || snake[snake.Count - 1].y < 0)
                        {
                            BoxView newbox = new BoxView { BackgroundColor = Color.FromHex("BEBEBE"), };
                            grid.Children.Add(newbox, snake[i-1].x, snake[i-1].y);
                            DependencyService.Get<IToast>().LongAlert("游戏结束");
                            for (int j = 4; j < snake.Count; j++)
                            {
                                grid.Children.RemoveAt(grid.Children.Count - 1);
                            }
                            snake = new List<snakemodel>{
                                new snakemodel{x=5,y=6 },
                                new snakemodel{x=6,y=6 },
                                new snakemodel{x=7,y=6 },
                                new snakemodel{x=8,y=6 },
                                new snakemodel{x=9,y=6 },
                            };
                            applemodel.x = new Random().Next(0, 50);
                            applemodel.y = new Random().Next(0, 50);
                            grid.Children.Add(apple, applemodel.x, applemodel.y);
                            direction = "right";
                            return;

                        }
                        BoxView box = new BoxView { BackgroundColor = Color.FromHex("BEBEBE"), };
                            grid.Children.Add(box, snake[i].x, snake[i].y);
                    }
                });
            };
            timer.Start();
        }
        protected override void OnDisappearing()
        {
            timer.Stop();
            base.OnDisappearing();
        }
    }
   
}