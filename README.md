### Merging av scene-filer (.unity) og prefab-filer

1. Installer [Perforce Helix Visual Merge Tool](https://www.perforce.com/downloads/visual-merge-tool). (Etter å ha valgt family og platform og trykket på download, kan du trykke på "Already a customer? __Skip registration__".)
   Evt. [direkte-link til versjon 2017.3](http://www.perforce.com/downloads/perforce/r17.3/bin.ntx64/p4vinst64.exe).
   
   Helst installer i default-stedet (`C:\Program Files\` eller `C:\Program Files (x86)\`), hvis ikke må du erstatte `%programs%\` i steget under.

2. Åpne fila `mergespecfile.txt` som ligger i `C:\Program Files\Unity\Editor\Data\Tools`, og erstatt disse linjene:
   ```
   unity use "%programs%\YouFallbackMergeToolForScenesHere.exe" "%l" "%r" "%b" "%d"
   prefab use "%programs%\YouFallbackMergeToolForPrefabsHere.exe" "%l" "%r" "%b" "%d"
   ```
   med disse:
   ```
   unity use "%programs%\Perforce\p4merge.exe" "%b" "%r" "%l" "%d"
   prefab use "%programs%\Perforce\p4merge.exe" "%b" "%r" "%l" "%d"
   ```
   Evt. kan du bare kommentere ut de to linjene som var der fra før med en `#` foran hver av dem, og legge til de to linjene over, istedenfor.

3. Gå til mappen hvor hele spillet ligger og gå til `MyFirstGame/.git`. Åpne `config`-fila og legg til disse linjene, f.eks. i bunnen av fila:
   ```
   [merge]
   	tool = unityyamlmerge

   [mergetool "unityyamlmerge"]
   	trustExitCode = false
   	cmd = 'C:/Program Files/Unity/Editor/Data/Tools/UnityYAMLMerge.exe' merge -p "$BASE" "$REMOTE" "$LOCAL" "$MERGED"
   ```
   Hvis Unity er installert et annet sted enn i `C:/Program Files/`, må den biten over erstattes med den riktige plasseringen.

---

Fra nå av, hver gang det oppstår en merge conflict kan du åpne git bash, som du i GitHub Desktop kan gjøre ved å trykke her:

![GitHub Desktop - git bash](https://i.imgur.com/vl9tgww.png)

eller i Git Extensions ved å trykke her:

![Git Extensions - git bash](https://i.imgur.com/bZ4HAtv.png)

I git bash kan du skrive `git mergetool`, som vil åpne Perforce P4Merge. Her trenger du bare å trykke på "Save"-knappen øverst til venstre, og deretter lukke vinduet.
- For hver konflikt, f.eks. at posisjonen til samme objekt har blitt endret på i begge branchene, vil den *nyeste endringen* bli tatt vare på når du trykker "Save".

Dermed er konflikten løst. Dette vil som regel opprette en ny `.orig`-fil per konflikt, som det bare er å slette før du commiter.
