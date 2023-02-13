<!--include all the content in connectins page for connection to database, if failed, fatal error will show-->
<?php require_once("includes/connection.php"); ?>
<!--include all the content in functions page to run functions, if failed, fatal error will show-->
<?php require_once("includes/functions.php"); ?>

<?php
//run the checkSession function to check if the session exists
checkSession();
?>

<?php  
    //get page id and store in var
    $id=$_GET['page'];

    //if right page found to delete, then delete
    if($page=get_page_by_id($id)){
        //query = mySQL = delete ow from oages where id = id
        $query="DELETE FROM pages WHERE id = " . $id;
        $result=mysqli_query($connection, $query);
        //if found something then deleted, redirect to content.php
            if(mysqli_affected_rows($connection)==1){
                header("location: content.php");
                exit;
                //otherwise delete failed
            }else{
                echo "<p> Delete failed". mysqli_error($connection)."</p>";
                echo "<a href=\"content.php\"> Return to the main Page";
            }//if doesnt exist, redirect to content.php
    }else{
        header("location: cotent.php");
        exit;
    }
?>

<!--close the connection-->
<?php mysql_close($connection); ?>
        