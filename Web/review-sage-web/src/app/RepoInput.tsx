import { useFormState } from "react-dom";
import styles from "./page.module.css";
import { startPRReview } from "./startPRReview";


export function RepoInput(data: any) {

  const [state, action] = useFormState(startPRReview, null, 'n/a');
  console.log('state', state);

  return <>
    <div className={styles.center}>
      <form action={action}>
        <input name="pr-number-to-review" placeholder="PR number to review" defaultValue={data?.prNumber}></input>
        <input name="git-repo-name" placeholder="PR repo" defaultValue={data?.repoName}></input>
        <input name="git-personal-token" placeholder="GitHub personal token" defaultValue={data?.personalToken}></input>
        <button>Improve the PR</button>
      </form>
    </div>
  </>
}