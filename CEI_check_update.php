<?php

$version = $_GET["v"];

switch($version){
	case "0.3":
		echo "0.4;https://github.com/Guillaume201/Importer-Hub-for-CRYENGINE/releases/tag/0.4";
		break;
	case "0.2":
		echo "0.3;https://github.com/Guillaume201/Importer-Hub-for-CRYENGINE/releases/tag/0.3";
		break;
}