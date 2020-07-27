jq -c '.frames[]' Blocksheet.json | while read i; do
    filename=$(basename $(echo $i | jq -r -c '.filename') '.png')
    x=$(echo $i | jq -r -c '.frame.x')
    y=$(echo $i | jq -r -c '.frame.y')
    w=$(echo $i | jq -r -c '.frame.w')
    h=$(echo $i | jq -r -c '.frame.h')

    echo "  - Name: ${filename}"
    echo "    X: ${x}"
    echo "    Y: ${y}"
    echo "    Width: ${w}"
    echo "    Height: ${h}"
done