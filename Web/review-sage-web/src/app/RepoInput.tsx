'use client'

import { useFormState } from "react-dom";
import styles from "./page.module.css";
import { startPRReview } from "./startPRReview";
import { useEffect, useState } from "react";

export function RepoInput(data: any) {

  const [state, action] = useFormState(startPRReview, null, 'n/a');
  const [improvingFlag, setImprovingFlag] = useState(false);
  console.log('state', state);

   const [prompt, setPrompt] = useState(() => {    
    if (typeof window === 'undefined') {
      return 'loading...';
    }
    const savedPrompt = localStorage.getItem('pr-prompt');    
    return savedPrompt || data?.prompt || '';
  });

   // Handle textarea change
   const handlePromptChange = (event:any) => {
    console.log('event.target.value', event.target.value)
    localStorage.setItem('pr-prompt', prompt);
    setPrompt(event.target.value);
  };
  

  const handleSubmit = (event:any) => {
    setImprovingFlag(true);
  };

  useEffect(() => {
    if (state != null && improvingFlag == true) {
      setImprovingFlag(false);
    }
  }, [state]); 


  return <div className={styles.prSection}>
    <div >
      <form action={action} onSubmit={handleSubmit}>
        <div className={styles.repoInput}>
            <textarea 
              name="pr-prompt" 
              placeholder="Prompt"
              className={styles.inputPrompt}              
              onChange={handlePromptChange}
              value = {prompt}
            />
        </div>
      </form>
    </div>
    
  </div>
}