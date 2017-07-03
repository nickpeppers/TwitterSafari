#!/bin/bash

#echo "PWD=$PWD"
#echo "HOME=$HOME"

if [[ $PWD/ = $HOME/* ]]; then 
	
	echo "Removing bin and obj subdirectories from [$PWD] ..."
	find . -iname "bin" -o -iname "obj" | xargs rm -rf

	echo "Done."
else
	echo "ERROR: $0 can only be run from your user directory."
fi


