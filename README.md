## Merging av scene-filer (.unity) og prefab-filer

Forutsetter at du har installert Git og helst også KDiff3, som kan gjøres gjennom [Git Extensions](https://gitextensions.github.io)-installeren, hvor du kan krysse av for å installere Git og KDiff3. Kan gjerne installeres i tillegg til [GitHub Desktop](https://desktop.github.com).

Evt. link for å installere dem for seg: [Git](https://git-scm.com/downloads) og [KDiff3](https://sourceforge.net/projects/kdiff3/files/latest/download?source=files).

### Oppsett

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

3. Gå til mappen hvor hele spillet ligger og gå til `Defendy-Pengy/.git`; `.git` er en skjult mappe, så du må vise skjulte filer før den dukker opp.
   Åpne `config`-fila og legg til disse linjene, f.eks. i bunnen av fila:
   ```
   [merge]
   	tool = unityyamlmerge

   [mergetool "unityyamlmerge"]
   	trustExitCode = false
   	cmd = 'C:/Program Files/Unity/Editor/Data/Tools/UnityYAMLMerge.exe' merge -p "$BASE" "$REMOTE" "$LOCAL" "$MERGED"
   ```
   Hvis Unity er installert et annet sted enn i `C:/Program Files/`, må den biten over erstattes med den riktige plasseringen.

---

Fra nå av, hver gang det oppstår en merge conflict med .unity- eller .prefab-filer kan du starte Git, som du i GitHub Desktop kan gjøre ved å trykke "Repository→Open in Command Prompt" *(evt. "Open in Git Bash" som du kan endre under "File→Options→Advanced→Shell")*:

![GitHub Desktop - git bash](https://i.imgur.com/vl9tgww.png)

eller i Git Extensions ved å trykke på "Git bash"-knappen:

![Git Extensions - git bash](https://i.imgur.com/bZ4HAtv.png)

Når du har startet Git kan du skrive `git mergetool`, som vil åpne Perforce P4Merge.
  - Hvis det er konflikt med .meta-filer og den sier at filen "seems unchanged" og spør "Was the merge successful?", svar nei (n); evt. svar ja (y) hvis den spør "Continue merging other unresolved paths?". Etter å ha løst resten av konfliktene kan du løse de .meta-fil-konfliktene du hoppet over. Dette kan du gjøre manuelt eller med KDiff3 ved å skrive:

    `git mergetool --tool=kdiff3`, eller `git mergetool -t kdiff3`

  - Hvis det er konflikt med vanlige kodefiler, som .cs-filer, sørg for at det er de endringene du ønsker å ta vare på som er markert i grønt i nederste del av P4Merge-vinduet. Hvis ikke, fjern alle linjene innenfor seksjonen med rødt omriss og skriv koden du egentlig vil ta vare på.

Ellers trenger du i P4Merge bare å trykke på "Save"-knappen øverst til venstre, og deretter lukke vinduet.
  - For hver konflikt, f.eks. at posisjonen til samme objekt har blitt endret på i begge branchene, vil den *nyeste endringen* bli tatt vare på når du trykker "Save".

Dermed er konflikten løst. Dette vil som regel opprette en ny .orig-fil per konflikt, som det bare er å slette før du commiter.
