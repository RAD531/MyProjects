<!--include all the content in connectins page for connection to database, if failed, fatal error will show-->
<?php require_once("includes/connection.php"); ?>
<!--include all the content in functions page to run functions, if failed, fatal error will show-->
<?php require_once("includes/functions.php"); ?>

<?php
//run the selected page function from functions
//grab data from page id, if found
find_selected_page();
?>

<?php
//run the checkSession function to check if the session exists
checkSession();
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

                    </td>
                    <td id="page">

                    <h2>Add page</h2>
                        <!--call create_page.php to send data to database on submit-->
                        <form action="create_page.php" method="post">
                            <p>
                                <!--default values-->
                                page name:<input type="text" name="menu_name" value="" id="menu_name" />
                            </p>
                            <p>Content:<br />
                                <textarea name="content" rows="20" cols="80"></textarea>
                            </p>
                            <input type="submit" value="Add Page" />
                        </form>
                        <!--cancel link-->
                        <a href="content.php">Cancel</a>
                    </td>
                </tr>
            </table>
  
    
<!--include syntax from footer-->
<?php include("includes/footer.php");?>
        