#!/bin/bash

for base in `seq 210 220` ; do  
	for ip in `seq 1 254`; do
		ping -c 1 $1.$base.$ip | grep "64 bytes" | cut  -d  " " -f 4 | tr -d ":" &
	done 
done

