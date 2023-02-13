<!--include all the content in connectins page for connection to database, if failed, fatal error will show-->
<?php require_once("includes/connection.php"); ?>
<!--include all the content in functions page to run functions, if failed, fatal error will show-->
<?php require_once("includes/functions.php"); ?>

<?php
//run the checkSession function to check if the session exists
checkSession();
?>

<?php 
//if submit button is true on form
    if(isset($_POST['submit'])){
        //id = get page id from server
        $id=$_GET['page'];
        // Menu_name = id Menu_name - to be posted to server
        $Menu_name=$_POST['Menu_name'];
        //content = id content - to be posted to server
        $content=$_POST['content'];

         //if the menu_name inut box is blank then give it a default name
        if ($Menu_name==null){
            $Menu_name="blank name given";
        }

        //query = mySQL query
        //query = update Menu_name and content where the id = the page id
        $query="UPDATE pages SET Menu_name='{$Menu_name}', content='{$content}' WHERE id={$id}";
        //apply the changes
        $result=mysqli_query($connection, $query);
    }
?>



<?php
//run the find_selected_page function
//get the correct page id from databse to be displayed
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

                    </td>
                    <td id = "page">

                    <!--get the current menu name in sel page array and diplay in header-->
                    <h2>Edit page: <?php echo $sel_page['Menu_name'];?></h2>
                        <!--edit page but the with id in URL to edit the correct page, use post to post any changes to server-->
                        <form action="edit_page.php?page=<?php echo urlencode($sel_page['id']); ?>" method="post">
                            <p>
                                <!--input box with menu name data from sel page array -->
                                page name:<input type="text" name="Menu_name" value="<?php echo $sel_page['Menu_name']; ?>" id="Menu_name" />
                            </p>

                            <p>Content:<br />
                                <!--text area which has content data pasted in from sel page array -->
                                <textarea name="content" rows="20" cols="80"><?php echo $sel_page['content']; ?></textarea>
                            </p>
                            <!--submit button which turns the form to true to run post if condition -->
                            <input type="submit" name="submit" value="Edit Page" />
                            &nbsp:&nbsp;
                            <!--open delete_page.php with sel page id number -->
                            <a href="delete_page.php?page=<?php echo $sel_page['id']; ?>"> Delete Page</a>
                        </form>
                        <br/>
                        <!--if user presse cancel, send to content.php, form not submitted -->
                        <a href="content.php">Cancel</a>
                    </td>
                </tr>
            </table>
  
    
<!--include the footer page syntax, dont kill the page if failed-->
<?php include("includes/footer.php");?>
        