<!--include all the content in functions page to run functions, if failed, fatal error will show-->
<?php require_once("includes/functions.php"); ?>

<?php
//run the checkSession function to check if the session exists
checkSession();
?>

<!--include the header page syntax, if unsucessfull, dont kil the page-->
<?php include("includes/header.php"); ?>
    <!--create a table to hold structure of page including navigation and page content-->
    <table id="structure">
        <tr>
            <td id ="navigation">
                &nbsp;
            </td>
            <td id ="page">
                <h2>Staff Menu</h2>
                <p id="Welcome">Welcome to the staff area.
                    <!--print the value of the username in the super global variable - session -->
                    <!-- should be gained by login username value -->
                    <?php echo $_SESSION['user_name']; ?>
                </p>
            <ul>
                <li><a href="content.php">Manage website content</a></li>
                <li><a href="add_user.php">Add staff user</a></li>
                <li><a href="logout.php">Logout</a></li>
            </ul>
            </td>
        </tr>
    </table>

<!--include the footer page syntax, dont kill the page if failed-->
<?php include("includes/footer.php"); ?>