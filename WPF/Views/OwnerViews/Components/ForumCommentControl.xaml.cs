using BookingApp.Application.UseCases;
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

public partial class ForumCommentControl : UserControl
{
    private readonly ForumComment comment;
    private readonly User activeUser;

    public string Username => comment.User.Username;
    public string Comment => comment.Content;

    public List<ForumCommentReport> reports;
    public string NumberOfReports => reports.Count > 0 ? string.Format("{0}🎌", reports.Count) : "";

    public ForumCommentControl(ForumComment comment, User activeUser, bool EnableReportButton)
    {
        InitializeComponent();
        this.comment = comment;
        this.activeUser = activeUser;
        DataContext = this;
        ReportButton.Visibility = EnableReportButton ? Visibility.Visible : Visibility.Collapsed;
        ReportCountText.Visibility = comment.User.Type == BookingApp.Resources.Types.UserType.Guest ? Visibility.Visible : Visibility.Collapsed;    

        reports = ForumCommentReportService.GetInstance().GetByForumCommentId(comment.Id);
    }

    private void ReportButton_MouseDown(object sender, MouseButtonEventArgs e)
    {
        ForumCommentReportService.GetInstance().Add(new ForumCommentReport()
        {
            ForumCommentId = comment.Id,
            UserId = UserService.GetInstance().GetById(activeUser.Id).Id
        });

        reports = ForumCommentReportService.GetInstance().GetByForumCommentId(comment.Id);
        ReportCountText.Text = string.Format("{0}🎌", reports.Count);
        ReportButton.Visibility = Visibility.Collapsed;
    }
}
