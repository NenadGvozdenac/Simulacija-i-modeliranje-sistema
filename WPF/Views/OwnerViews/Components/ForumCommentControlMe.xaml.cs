using BookingApp.Domain.Models;
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

namespace BookingApp.WPF.Views.OwnerViews.Components;

public partial class ForumCommentControlMe : UserControl
{
    private readonly ForumComment comment;
    public string Username => comment.User.Username;
    public string Comment => comment.Content;

    public ForumCommentControlMe(ForumComment comment)
    {
        InitializeComponent();
        this.comment = comment;
        DataContext = this;
    }
}
