﻿using AngleSharp;
using Scrapper.Application.Dtos;
using Scrapper.Application.Scrappers.DealerRater;
using System.Threading.Tasks;
using Xunit;

namespace Scrapper.Application.Tests.Scrappers.DealerRater
{
    public class DealerRaterScrapperTests
    {
        [Fact]
        public async ValueTask ParseReview_Should_Be_Ok()
        {
            //Arrange
            var source = @"
<div class=""review-entry col-xs-12 text-left pad-none pad-top-lg  border-bottom-teal-lt"">
<a name=""r8874825""></a>
<div class=""col-xs-12 col-sm-3 pad-left-none text-center review-date margin-bottom-md"">
    <div class=""italic col-xs-6 col-sm-12 pad-none margin-none font-20"">November 16, 2021</div>
    <div class=""col-xs-6 col-sm-12 pad-none dealership-rating"">
        <div class=""rating-static visible-xs pad-none margin-none rating-50 pull-right""><span style=""display: none;"" class=""ae-compliance-indent ae-reader-visible""> 5 out of 5 Stars </span></div>
        <div class=""rating-static hidden-xs rating-50 margin-center""><span style=""display: none;"" class=""ae-compliance-indent ae-reader-visible""> 5 out of 5 Stars </span></div>
        <div class=""col-xs-12 hidden-xs pad-none margin-top-sm small-text dr-grey"">SALES VISIT - NEW</div>
    </div>
</div>
<div class=""col-xs-12 col-sm-9 pad-none review-wrapper"">
<!-- REVIEW TITLE, USER-->
<div class=""margin-bottom-sm line-height-150"">
    <h3 class=""no-format inline italic-bolder font-20 dark-grey"">""We have finally met the sales team and it was...""</h3>
    <span class=""italic font-18 black notranslate"">- brendareynolds</span>
</div>

<!-- REVIEW BODY -->

<div class=""tr margin-top-md"">
    <div class=""td text-left valign-top "">
        <p class=""font-16 review-content margin-bottom-none line-height-25"">We have finally met the sales team and it was FAN-xxxxxx-TASTIC!! We got to buy a new vehicle yesterday! Jeannie Evans is a gem! She helped me pick the truck we wanted, Freddie Thomlinson went above and beyond to help me find discounts to lower our finance amount and Taylor helped us to put the final touches in finance. If you are looking to buy from genuine people and want a GREAT deal, you most definately want to see these guys.  You will NOT regret it! 10 stars out of 5!!</p>
            <a id=""8874825"" class=""read-more-toggle pointer line-height-25 small-text block margin-bottom-md"" data-ae-append="""" role=""button"" tabindex=""0"" data-ae-blurbtype=""button"" aria-expanded=""false"">Read More<span style=""display: none;"" class=""ae-compliance-indent""> ""We have finally met the sales team and it was..."" </span></a>
    </div>
</div>

<!-- REVIEW RATINGS - ALL -->
<div class=""pull-left pad-left-md pad-right-md bg-grey-lt margin-bottom-md review-ratings-all review-hide"">
    <!-- REVIEW RATING - CUSTOMER SERVICE -->
    <div class=""table width-100 pad-left-none pad-right-none margin-bottom-md"">
            <div class=""tr"">
                <div class=""lt-grey small-text td"">Customer Service</div>
                <div class=""rating-static-indv rating-50 margin-top-none td""></div>
            </div>
                            <!-- REVIEW RATING - FRIENDLINESS -->
            <div class=""tr margin-bottom-md"">
                <div class=""lt-grey small-text td"">Friendliness</div>
                <div class=""rating-static-indv rating-50 margin-top-none td""></div>
            </div>
                    <!-- REVIEW RATING - PRICING -->
            <div class=""tr margin-bottom-md"">
                <div class=""lt-grey small-text td"">Pricing</div>
                <div class=""rating-static-indv rating-50 margin-top-none td""></div>
            </div>
                    <!-- REVIEW RATING - EXPERIENCE -->
            <div class=""tr margin-bottom-md"">
                <div class=""td lt-grey small-text"">Overall Experience</div>
                <div class=""rating-static-indv rating-50 margin-top-none td""></div>
            </div>
        <!-- REVIEW RATING - RECOMMEND DEALER -->
        <div class=""tr"">
            <div class=""lt-grey small-text td"">Recommend Dealer</div>
            <div class=""td small-text boldest"">
                Yes
            </div>
        </div>
    </div>


</div>

<!-- EMPLOYEE SECTION -->
<div class=""clear-fix  margin-top-sm"">
        <div class=""col-xs-12 lt-grey pad-left-none employees-wrapper"">
            <div class=""small-text"">Employees Worked With </div>

                         <div class=""col-xs-12 col-sm-6 col-md-4 pad-left-none pad-top-sm pad-bottom-sm review-employee"">
                             <div class=""table"">
                                 <div class=""td square-image employee-image"" style=""background-image: url(https://cdn-user.dealerrater.com/images/dealer/23685/employees/95114f8d90fc.jpg)""></div>
                                 
                                 <div class=""td valign-bottom pad-left-md pad-top-none pad-bottom-none"">
                                         <a class=""notranslate pull-left line-height-1 tagged-emp small-text teal  margin-right-sm emp-712247"" data-emp-id=""712247"" href=""/sales/Jeannie-Evans-review-712247/"">
                                             Jeannie Evans
                                         </a>
                                                                              <div class=""col-xs-12 pad-none margin-none pad-top-sm"">


<div class=""relative employee-rating-badge-sm"">
    <div class=""col-xs-12 pad-none"">
            <span class=""pull-left font-14 boldest lt-grey line-height-1 pad-right-sm margin-right-sm border-right"">5.0</span>
            <div class=""rating-static rating-50 margin-top-none pull-left""><span style=""display: none;"" class=""ae-compliance-indent ae-reader-visible""> 5 out of 5 Stars </span></div>
    </div>
    
</div>

                                         </div>
                                 </div>

                             </div>

                         </div>
                         <div class=""col-xs-12 col-sm-6 col-md-4 pad-left-none pad-top-sm pad-bottom-sm review-employee"">
                             <div class=""table"">
                                 <div class=""td square-image employee-image"" style=""background-image: url(https://cdn-user.dealerrater.com/images/dealer/23685/employees/ca22768af3f7.jpg)""></div>
                                 
                                 <div class=""td valign-bottom pad-left-md pad-top-none pad-bottom-none"">
                                         <a class=""notranslate pull-left line-height-1 tagged-emp small-text teal  margin-right-sm emp-640356"" data-emp-id=""640356"" href=""/sales/Taylor-Prickett-review-640356/"">
                                             Taylor Prickett
                                         </a>
                                                                              <div class=""col-xs-12 pad-none margin-none pad-top-sm"">


<div class=""relative employee-rating-badge-sm"">
    <div class=""col-xs-12 pad-none"">
            <span class=""pull-left font-14 boldest lt-grey line-height-1 pad-right-sm margin-right-sm border-right"">5.0</span>
            <div class=""rating-static rating-50 margin-top-none pull-left""><span style=""display: none;"" class=""ae-compliance-indent ae-reader-visible""> 5 out of 5 Stars </span></div>
    </div>
    
</div>

                                         </div>
                                 </div>

                             </div>

                         </div>
                         <div class=""col-xs-12 col-sm-6 col-md-4 pad-left-none pad-top-sm pad-bottom-sm review-employee"">
                             <div class=""table"">
                                 <div class=""td square-image employee-image"" style=""background-image: url(https://cdn-user.dealerrater.com/images/dealer/23685/employees/fcea1d8f0d9d.jpg)""></div>
                                 
                                 <div class=""td valign-bottom pad-left-md pad-top-none pad-bottom-none"">
                                         <a class=""notranslate pull-left line-height-1 tagged-emp small-text teal  margin-right-sm emp-479220"" data-emp-id=""479220"" href=""/sales/Freddie-Tomlinson-review-479220/"">
                                             Freddie Tomlinson
                                         </a>
                                                                              <div class=""col-xs-12 pad-none margin-none pad-top-sm"">


<div class=""relative employee-rating-badge-sm"">
    <div class=""col-xs-12 pad-none"">
            <span class=""pull-left font-14 boldest lt-grey line-height-1 pad-right-sm margin-right-sm border-right"">5.0</span>
            <div class=""rating-static rating-50 margin-top-none pull-left""><span style=""display: none;"" class=""ae-compliance-indent ae-reader-visible""> 5 out of 5 Stars </span></div>
    </div>
    
</div>

                                         </div>
                                 </div>

                             </div>

                         </div>
                         <div class=""col-xs-12 col-sm-6 col-md-4 pad-left-none pad-top-sm pad-bottom-sm review-employee"">
                             <div class=""table"">
                                 <div class=""td square-image employee-image"" style=""background-image: url(https://cdn-user.dealerrater.com/images/dealer/23685/employees/b5f1378d2169.jpg)""></div>
                                 
                                 <div class=""td valign-bottom pad-left-md pad-top-none pad-bottom-none"">
                                         <a class=""notranslate pull-left line-height-1 tagged-emp small-text teal  margin-right-sm emp-631679"" data-emp-id=""631679"" href=""/sales/Patrick-Evans-review-631679/"">
                                             Patrick Evans
                                         </a>
                                                                              <div class=""col-xs-12 pad-none margin-none pad-top-sm"">


<div class=""relative employee-rating-badge-sm"">
    <div class=""col-xs-12 pad-none"">
            <span class=""pull-left font-14 boldest lt-grey line-height-1 pad-right-sm margin-right-sm border-right"">5.0</span>
            <div class=""rating-static rating-50 margin-top-none pull-left""><span style=""display: none;"" class=""ae-compliance-indent ae-reader-visible""> 5 out of 5 Stars </span></div>
    </div>
    
</div>

                                         </div>
                                 </div>

                             </div>

                         </div>
                         <div class=""col-xs-12 col-sm-6 col-md-4 pad-left-none pad-top-sm pad-bottom-sm review-employee"">
                             <div class=""table"">
                                 <div class=""td square-image employee-image"" style=""background-image: url(https://cdn-user.dealerrater.com/images/dealer/23685/employees/91fda03d9e5b.jpg)""></div>
                                 
                                 <div class=""td valign-bottom pad-left-md pad-top-none pad-bottom-none"">
                                         <a class=""notranslate pull-left line-height-1 tagged-emp small-text teal   emp-467062"" data-emp-id=""467062"" href=""/sales/Mariela-Hernandez-review-467062/"">
                                             Mariela Hernandez
                                         </a>
                                                                              <div class=""col-xs-12 pad-none margin-none pad-top-sm"">


<div class=""relative employee-rating-badge-sm"">
    <div class=""col-xs-12 pad-none"">
            <span class=""pull-left font-14 boldest lt-grey line-height-1 pad-right-sm margin-right-sm border-right"">5.0</span>
            <div class=""rating-static rating-50 margin-top-none pull-left""><span style=""display: none;"" class=""ae-compliance-indent ae-reader-visible""> 5 out of 5 Stars </span></div>
    </div>
    
</div>

                                         </div>
                                 </div>

                             </div>

                         </div>
                    </div>
</div>

<!-- SOCIAL MEDIA AND REVIEW ACTIONS -->
<div class=""col-xs-12 pad-none review-hide margin-top-lg"">
    <div class=""pull-left"">
        <a href=""https://twitter.com/intent/tweet?url=http://www.dealerrater.com/consumer/social/8874825&amp;via=dealerrater&amp;text=Check+out+the+latest+review+on+McKaig+Chevrolet+Buick+-+A+Dealer+For+The+People"" onclick=""window.open('https://twitter.com/intent/tweet?url=http://www.dealerrater.com/consumer/social/8874825&amp;via=dealerrater&amp;text=Check+out+the+latest+review+on+McKaig+Chevrolet+Buick+-+A+Dealer+For+The+People', 'sharer', 'toolbar=0,status=0,width=750,height=500');return false;"" target=""_blank"" rel=""nofollow"" aria-describedby=""audioeye_new_window_message"" onkeypress=""if (event.key === &quot;Enter&quot; || event.key === &quot; &quot;) { event.target.onclick(event); }""><img class=""align-bottom"" height=""20"" src=""https://www.dealerrater.com/ncdn/s/206.20211119.1/Graphics/icons/icon_twitter_sm.png"" alt=""Twitter Social Network""></a>
        <a href=""http://www.facebook.com/share.php?u=http://www.dealerrater.com/consumer/social/8874825"" onclick=""window.open('http://www.facebook.com/share.php?u=http://www.dealerrater.com/consumer/social/8874825', 'sharer', 'toolbar=0,status=0,width=750,height=500');return false;"" target=""_blank"" rel=""nofollow"" aria-describedby=""audioeye_new_window_message"" onkeypress=""if (event.key === &quot;Enter&quot; || event.key === &quot; &quot;) { event.target.onclick(event); }""><img class=""align-bottom"" height=""20"" src=""https://www.dealerrater.com/ncdn/s/206.20211119.1/Graphics/icons/icon_facebook_sm.png"" alt=""Facebook Social Network""></a>
    </div>
    <div class=""pull-left margin-left-md"">
        <a href=""#"" onclick=""javascript:window.reportReview(8874825); return false;"" class=""small-text"" onkeypress=""if (event.key === &quot;Enter&quot; || event.key === &quot; &quot;) { event.target.onclick(event); }"">Report</a> |
        <a href=""#"" onclick=""window.open('/consumer/dealer/23685/review/8874825/print', 'report', 'toolbar=no,scrollbars=yes,location=no,width=720,height=400,resizable=yes'); return false;"" class=""small-text"" onkeypress=""if (event.key === &quot;Enter&quot; || event.key === &quot; &quot;) { event.target.onclick(event); }"">Print</a>
    </div>
</div>

<!-- PUBLIC MESSAGES -->

<!-- WAS HELPFUL SECTION -->
<div class=""col-xs-12 margin-bottom-lg"">
    <div class=""pull-right"">
        <a href=""#"" class=""helpful-button"" onclick=""javascript:MarkReviewHelpful(8874825, this); return false;"" onkeypress=""if (event.key === &quot;Enter&quot; || event.key === &quot; &quot;) { event.target.onclick(event); }"">
            <img class=""pull-left margin-right-sm"" src=""https://www.dealerrater.com/ncdn/s/206.20211119.1/Graphics/icons/icon-thumbsup.png"" alt=""""> Helpful <span class=""helpful-count display-none"" id=""helpful_count_8874825"">0</span></a>
    </div>
</div>
</div>

</div>";

            //Use the default configuration for AngleSharp
            var config = Configuration.Default;

            //Create a new context for evaluating webpages with the given config
            var context = BrowsingContext.New(config);

            //Just get the DOM representation
            var document = await context.OpenAsync(req => req.Content(source));

            //Act
            var reviewParsed = DealerRaterScrapper.ParseReview(document.DocumentElement)!;

            //Assert
            var reviewToCompare = new ReviewEntry("November 16, 2021",
                "brendareynolds",
                @"""We have finally met the sales team and it was...""", 
                "We have finally met the sales team and it was FAN-xxxxxx-TASTIC!! We got to buy a new vehicle yesterday! Jeannie Evans is a gem! She helped me pick the truck we wanted, Freddie Thomlinson went above and beyond to help me find discounts to lower our finance amount and Taylor helped us to put the final touches in finance. If you are looking to buy from genuine people and want a GREAT deal, you most definately want to see these guys.  You will NOT regret it! 10 stars out of 5!!",
                0);
            
            Assert.Equal(reviewToCompare, reviewParsed);
        }
    }
}