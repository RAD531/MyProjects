<!--include all the content in connectins page for connection to database, if failed, fatal error will show-->
<?php require_once("includes/connection.php"); ?>
<!--include all the content in functions page to run functions, if failed, fatal error will show-->
<?php require_once("includes/functions.php"); ?>

<?php
//run the checkSession function to check if the session exists
checkSession();
?>

<?php
//run the selected page function from functions
//grab data from page id, if found
find_selected_page(); 
?>

<?php include("includes/header.php");?>


            <table id = "structure">
                <tr>
                    <td id = "navigation">
                    
                    <?php
                    //run the navigation function
                    //grabs all the Menu names from pages table in database and link to page ID
                    //echo the found list and display in UL in navugaton table 
                     navigation();
                    ?>
                   
                    <br/>
                    <!--link to new_page.php to create a new page-->
                    <a href="new_page.php">+Add a new page</a>
                    <br/>

                    <br/>
                    <a href="staff.php">Back to staff page</a>

                    </td>
                    <td id="page">

                    <?php 
                    //if sel_page contains data, run the following code
                    if (!is_null($sel_page)){
                        //echo the Menu name in header
                        echo "<h2>".$sel_page['Menu_name']."</h2>";
                        //echo the page content from sel_page array in page content div
                        echo "<div class =\"page-content\">";
                        echo $sel_page['content'];
                        echo "</div>";
                        //create a link to the edit page but with the page id from the page user has clicked on
                        echo "<a href=\"edit_page.php?page={$sel_page['id']}\">Edit the page</a>";
                        echo "&nbsp:&nbsp";
                        //create a link to the delete page but with the page id from the page user has clicked on
                        echo "<a href=\"delete_page.php?page=".$sel_page['id']."\"> Delete page</a>";
                        //if sel page is null, then simply echo header to edit a page
                    }else{echo "<h2> select a page to edit</h2>";}

                    ?>
                    
                    </td>
                    
                </tr>
            </table>
  
    
<!--include the footer page syntax, dont kill the page if failed-->
<?php include("includes/footer.php");?>
        