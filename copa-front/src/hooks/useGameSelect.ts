import { useCallback, useState } from "react";

export default function useGameSelect(init: string[]) {
  const [value, setValue] = useState(init);

  const changeHandler = useCallback((id: string) => {
    const newValue = [...value];
    const existingIndex = value.findIndex(el => el === id);

    if (existingIndex >= 0) {
      newValue.splice(existingIndex, 1);
    } else if (newValue.length < 8) {
      newValue.push(id);
    }

    setValue(newValue);
  }, [value]);

  return [value, changeHandler] as const;
}
