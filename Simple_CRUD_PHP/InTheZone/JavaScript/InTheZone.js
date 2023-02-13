//start of calendar

function buildCal(m, y, cM, cH, cDW, cD, brdr){
  //set up months
  var mn=['January','February','March','April','May','June','July','August','September','October','November','December'];
  //num of days for each month with Jan first value
  var dim=[31,0,31,30,31,30,31,31,30,31,30,31];
  
  var oD = new Date(y, m-1, 1); //fix date bug when current day is 31st
  oD.od=oD.getDay()+1; //fix date bug when current day is 31st
  
  var todaydate=new Date()
  var scanfortoday=(y==todaydate.getFullYear() && m==todaydate.getMonth()+1)? todaydate.getDate() : 0
  
  dim[1]=(((oD.getFullYear()%100!=0)&&(oD.getFullYear()%4==0))||(oD.getFullYear()%400==0))?29:28;
  var t='<div class="'+cM+'"><table class="'+cM+'" cols="7" cellpadding="0" border="'+brdr+'" cellspacing="0"><tr align="center">';
  t+='<td colspan="7" align="center" class="'+cH+'">'+mn[m-1]+' - '+y+'</td></tr><tr align="center">';
  for(s=0;s<7;s++)t+='<td class="'+cDW+'">'+"SMTWTFS".substr(s,1)+'</td>';
  t+='</tr><tr align="center">';
  for(i=1;i<=42;i++){
  var x=((i-oD.od>=0)&&(i-oD.od<dim[m-1]))? i-oD.od+1 : '&nbsp;';
  if (x==scanfortoday)
  x='<span id="today">'+x+'</span>'
  t+='<td class="'+cD+'">'+x+'</td>';
  if(((i)%7==0)&&(i<36))t+='</tr><tr align="center">';
  }
  return t+='</tr></table></div>';
  }

//end of calendar

//start of product table sort------------------------------------------------------------------------

function sortTable(n) {
    var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
    table = document.getElementById("productTable");
    switching = true;
    //Set the sorting direction to ascending:
    dir = "asc"; 
    /*Make a loop that will continue until
    no switching has been done:*/
     while (switching) {
    //start by saying: no switching is done:
    switching = false;
    rows = table.rows;
    /*Loop through all table rows (except the
    first, which contains table headers):*/
    for (i = 1; i < (rows.length - 1); i++) {
    //start by saying there should be no switching:
    shouldSwitch = false;
    /*Get the two elements you want to compare,
    one from current row and one from the next:*/
    x = rows[i].getElementsByTagName("TD")[n];
    y = rows[i + 1].getElementsByTagName("TD")[n];
    /*check if the two rows should switch place,
    based on the direction, asc or desc:*/
    if (dir == "asc") {
    if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
    //if so, mark as a switch and break the loop:
    shouldSwitch= true;
    break;
    }
    } else if (dir == "desc") {
    if (x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase()) {
    //if so, mark as a switch and break the loop:
    shouldSwitch = true;
    break;
    }
    }
    }
    if (shouldSwitch) {
    /*If a switch has been marked, make the switch
    and mark that a switch has been done:*/
    rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
    switching = true;
    //Each time a switch is done, increase this count by 1:
    switchcount ++;      
    } else {
    /*If no switching has been done AND the direction is "asc",
    set the direction to "desc" and run the while loop again.*/
    if (switchcount == 0 && dir == "asc") {
    dir = "desc";
    switching = true;
    }
}
}
}


//end of product table sort--------------------------------------------------------------------------

//Function to scroll back to the top of screen--------------------------------------------------------
    var timeOut;
    function scrollToTop() {
    if (document.body.scrollTop!=0 || document.documentElement.scrollTop!=0){
        window.scrollBy(0,-30);
        timeOut=setTimeout('scrollToTop()',10);
    }
    else clearTimeout(timeOut);
    }
//End Function to scroll back to the top of screen--------------------------------------------------------

//start of get day function--------------------------------------------------------------------------------
    
var day;

switch (new Date().getDay())
{
    case 0:
        day = "<h1>Sunday</h1><p>We are closed</p>"
break;
    case 1:
        day = "<h1>Monday</h1><p>We are open today from:</p><p>9.00-16.00</p>";
 break;
    case 2: 
        day = "<h1>Tuesday</h1><p>We are open today from:</p><p>9.00-16.00</p>";
break;
    case 3:
        day = "<h1>Wednesday</h1><p>We are open today from:</p><p>9.00-16.00</p>";
break;
    case 4: 
        day = "<h1>Thursday</h1><p>We are open today from:</p><p>9.00-16.00</p>";
break;
    case 5:
        day = "<h1>Friday</h1><p>We are open today from:</p><p>9.00-16.00</p>";
break;
    case 6: 
        day = "<h1>Saturday</h1><p>We are open today from:</p><p>9.00-16.00</p>";
}

//end of get day function-------------------------------------------------------------------------------

//function to display content depending on what button is pressed---------------------------------------
function openFindUs(evt, transportName) {

    var i, tabcontent, tablinks;

    tabcontent = document.getElementsByClassName("tabcontent");

    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    tablinks = document.getElementsByClassName("tablinks");

    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    document.getElementById(transportName).style.display = "block";

    evt.currentTarget.className += " active";
    }

    //end display content function-------------------------------------------------------------------

    //start of calendar------------------------------------------------------------------------------

    var themonths=['January','February','March','April','May','June',
                            'July','August','September','October','November','December']

                            var todaydate=new Date()
                            var curmonth=todaydate.getMonth()+1 //get current month (1-12)
                            var curyear=todaydate.getFullYear() //get current year

                        function updatecalendar(theselection){
                            var themonth=parseInt(theselection[theselection.selectedIndex].value)+1
                            var calendarstr=buildCal(themonth, curyear, "main", "month", "daysofweek", "days", 0)
                            if (document.getElementById)
                                document.getElementById("calendarspace").innerHTML=calendarstr
                        }
    //end calendar---------------------------------------------------------------------------------------

    function showContent()
            {
                
                /* Enables content to show when a button is pressed, normal state is hidden, the first function is for the first box in the "News and Events" section */

                var x = 
                document.getElementById("newsandeventscontent1");
                if (x.style.display ==="block")
                {
                    x.style.display = "none";    
                }
                else
                {
                    x.style.display = "block";
                }

                /* Enables button to change text when pressed */
            
                var buttonText = 
                document.getElementById("button1");
                if (buttonText.value=="Show More")
                { 
                    buttonText.value = "Show Less";
                }
                else
                {
                    buttonText.value = "Show More";
                }
            }

            function showContent2()
            {
            
             /* Enables content to show when a button is pressed, normal state is hidden, the below function is for the second box in the "News and Events" section */

            var y = 
                document.getElementById("newsandeventscontent2");
                if (y.style.display ==="block")
                {
                    y.style.display = "none";    
                }
                else
                {
                    y.style.display = "block";
                }

            /* Enables button to change text when pressed */

            var buttonText = 
                document.getElementById("button2");
                if (buttonText.value=="Show More")
                { 
                    buttonText.value = "Show Less";
                }
                else
                {
                    buttonText.value = "Show More";
                }

            }

            function showContent3()
            {
            
             /* Enables content to show when a button is pressed, normal state is hidden, the below function is for the second box in the "News and Events" section */

            var z = 
                document.getElementById("newsandeventscontent3");
                if (z.style.display ==="block")
                {
                    z.style.display = "none";    
                }
                else
                {
                    z.style.display = "block";
                }

             /* Enables button to change text when pressed */

             var buttonText = 
             document.getElementById("button3");
             if (buttonText.value=="Show More")
             { 
                 buttonText.value = "Show Less";
             }
             else
             {
                 buttonText.value = "Show More";
             }

         }

            // dipslays a message on the favicon toolbar when user leaves page
            
            window.onscroll = function() {myFunction()};

            function myFunction() {
            var winScroll = document.body.scrollTop || document.documentElement.scrollTop;
            var height = document.documentElement.scrollHeight - document.documentElement.clientHeight;
            var scrolled = (winScroll / height) * 100;
            document.getElementById("myBar").style.width = scrolled + "%";
            }

            