#!/bin/bash

for f in $(git ls-files -i -X .gitignore)
do
    git rm --cached $f
done
